using Microsoft.Win32;
using Newtonsoft.Json;
using NonInvasiveKeyboardHookLibrary;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PsDocker
{
    public partial class Form1 : Form
    {
        List<string> TotalPath = new List<string>();
        List<string> pathShown = new List<string>();
        List<string> pastPath = new List<string>();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParm);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "P'S_Dock";
        private static void SetStartup()
        {
            //Set the application to run at startup
            RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            key.SetValue(StartupValue, Application.ExecutablePath.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Button btn = new Button();
            //btn.BackColor = Color.Teal;
            //btn.Text = "Click";
            //btn.FlatAppearance.BorderSize = 0;
            //btn.ForeColor = Color.White;
            //btn.Name = "Button1";
            //btn.Size = new Size(300, 200);
            //btn.UseVisualStyleBackColor = false;
            //btn.FlatStyle = FlatStyle.Flat;
            //btn.TabIndex = 0;
            //btn.Location = new Point(0, 0);
            //btn.MouseDown += btnMouseDown;
            //flowLayoutPanel1.Controls.Add(btn);
            //CheckForIllegalCrossThreadCalls = false;
            DropGraphicsPanel.BringToFront();

            var keyboardHookManager = new KeyboardHookManager();
            keyboardHookManager.Start();
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.WindowsKey, 0x20, () =>
            {
                Invoke(toggleVisibility);
            });

            FormClosing += (s, e) =>
            {
                e.Cancel = true;
                HideWindow();
            };

            ImageList imageList = new ImageList();
            imageList.ImageSize = new(40, 40);
            imageList.Images.Add("file", Properties.Resources.File);
            imageList.Images.Add("folder", Properties.Resources.Folder);
            listView1.LargeImageList = imageList;

            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.Click += (sender, e) =>
            {
                toggleVisibility(true);
            };


            this.Text = string.Empty;
            this.ControlBox = false;
            listView1.ItemDrag += onItemDrag;
            listView1.DoubleClick += onDoubleClick;

            WindowState = FormWindowState.Minimized;
            Hide();

            var data = FileManager.LoadTxt("SavedPath");
            if (data != null)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                TotalPath = JsonConvert.DeserializeObject<List<string>>(data);
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            SetStartup();
            Reload();
        }

        private void toggleVisibility()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowWindow();
            }
            else
            {
                HideWindow();
            }
        }
        private void toggleVisibility(bool systemTrayClick = false)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowWindow(systemTrayClick);
            }
            else
            {
                HideWindow();
            }
        }
        private void onDoubleClick(object? sender, EventArgs e)
        {
            var path = listView1.SelectedItems[^1].SubItems[1].Text;

            bool filePath = File.Exists(listView1.SelectedItems[^1].SubItems[1].Text);
            bool folderPath = Directory.Exists(path);
            if (filePath)
            {
                using Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + listView1.SelectedItems[^1].SubItems[1].Text + "\"";
                fileopener.Start();
            }
            else if (folderPath)
            {
                pastPath.Add(path);
                TotalPath = Directory.GetDirectories(path).ToList();
                foreach (var item in Directory.GetFiles(path))
                {
                    TotalPath.Add(item);
                }
                Reload();
            }
        }

        private void Reload(bool HardReload = false)
        {
            homeImage.Visible = !HardReload;
            backImage.Visible = !HardReload;
            if (HardReload)
            {
                pastPath.Clear();
                var data = FileManager.LoadTxt("SavedPath");
                if (data != null)
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    TotalPath = JsonConvert.DeserializeObject<List<string>>(data);
#pragma warning restore CS8601 // Possible null reference assignment.
                }
            }

            pathShown.Clear();
            listView1.Clear();

            if (HardReload)
            {
                IsHome = true;
                foreach (var item in TotalPath)
                {
                    addItemToShow(item);
                }
            }
            else
            {
                IsHome = false;
                List<(DateTime dateTime, string path)> sortedPath = new();
                foreach (var item in TotalPath)
                {
                    var dateTime = File.GetCreationTime(item);
                    sortedPath.Add((dateTime, item));
                }

                var FinalList = sortedPath.OrderByDescending(x => x.dateTime).ToList();
                foreach (var item in FinalList)
                {
                    addItemToShow(item.path);
                }
            }
        }

        //Drag Out
        private void addFilesForDragOut(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length <= 0)
                return;
            DataObject dataObject = new DataObject(DataFormats.FileDrop, filePaths);
            DoDragDrop(dataObject, DragDropEffects.Copy);
        }

        //Drop In
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            DropGraphicsPanel.Visible = false;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (IsHome)
            {

                foreach (var path in fileList)
                {
                    addItemToShow(path);
                    TotalPath.Add(path);
                }

                List<string> newList = new List<string>();
                foreach (var item in TotalPath)
                {
                    if (newList.Contains(item) == false)
                        newList.Add(item);
                }
                FileManager.SaveTxt("SavedPath", JsonConvert.SerializeObject(newList));
            }
            else
            {
                var currentPath = pastPath[^1];

                foreach (var item in fileList)
                {
                    if (File.Exists(item))
                    {
                        currentPath += (@"\" + item.Split(@"\")[^1]);
                        string finalPath = currentPath;
                        int index = 0;
                        while (File.Exists(finalPath))
                        {
                            index++;
                            finalPath = currentPath + index;
                        }

                        Console.WriteLine(finalPath);
                        File.Copy(item, finalPath);
                    }
                    else if (Directory.Exists(item))
                        CopyFilesRecursively(item, currentPath);
                }

                SubPath(pastPath[^1]);
                Reload();
            }
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            var folderName = sourcePath.Split(@"\")[^1];
            targetPath += @"\\" + folderName;
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        public static void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        private void addItemToShow(string path)
        {
            bool fileExist = File.Exists(path);
            bool folderExist = Directory.Exists(path);
            if ((fileExist || folderExist) && pathShown.Contains(path) == false)
            {
                pathShown.Add(path);
                string _name = path.Split(@"\")[^1];
                var currentItem = new ListViewItem(_name);
                currentItem.SubItems.Add(path);
                currentItem.ImageKey = fileExist ? "file" : "folder";
                listView1.Items.Add(currentItem);
            }
        }

        private void onItemDrag(object? sender, ItemDragEventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                addFilesForDragOut(new[] { item.SubItems[1].Text });
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            DropGraphicsPanel.Visible = true;
            if (IsHome)
            {
                e.Effect = DragDropEffects.Link;
                DropGraphicsPanel.Image = Properties.Resources.LinkPanel;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
                DropGraphicsPanel.Image = Properties.Resources.CopyPanel;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
            else
            {
                HideWindow();
            }
        }

        private void ShowWindow(bool systemTrayClick = false)
        {
            Show();
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Cursor.Position.X - Width / 2, (systemTrayClick) ? (Cursor.Position.Y - Height - 30) : (Cursor.Position.Y - Height / 2));
            TopMost = true;
            if (IsHome || !Is_Pinned)
                Reload(true);
            else
            {
                SubPath(pastPath[^1]);
                Reload();
            }
        }

        private void HideWindow()
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void SubPath(string _path)
        {
            var TotalPath = Directory.GetDirectories(_path).ToList();
            foreach (var item in Directory.GetFiles(_path))
            {
                TotalPath.Add(item);
            }
            List<(DateTime dateTime, string path)> sortedPath = new();

            foreach (var item in TotalPath)
            {
                var dateTime = File.GetCreationTime(item);
                sortedPath.Add((dateTime, item));
            }

            var FinalList = sortedPath.OrderByDescending(x => x.dateTime).ToList();
            TotalPath.Clear();
            foreach (var item in FinalList)
            {
                TotalPath.Add(item.path);
            }

            this.TotalPath = TotalPath;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("HIII");
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listView1.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    contextMenuStrip1.Items[0].Enabled = IsHome;
                }
            }
        }

        bool IsHome = true;
        private void hiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsHome)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    if (TotalPath.Contains(item.SubItems[1].Text))
                    {
                        TotalPath.Remove(item.SubItems[1].Text);
                    }
                }
                FileManager.SaveTxt("SavedPath", JsonConvert.SerializeObject(TotalPath));
                Reload(true);
            }
        }

        private void homeImage_MouseEnter(object sender, EventArgs e)
        {
            homeImage.Image = Properties.Resources.HomeButtonInactive;
        }

        private void homeImage_MouseLeave(object sender, EventArgs e)
        {
            homeImage.Image = Properties.Resources.HomeButton;
        }

        private void homeImage_MouseClick(object sender, MouseEventArgs e)
        {
            Reload(true);
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            DropGraphicsPanel.Visible = false;
        }

        private void backImage_Click(object sender, EventArgs e)
        {
            goBack();
        }
        void goBack()
        {
            if (pastPath != null && pastPath.Count > 0)
                pastPath.RemoveAt(pastPath.Count - 1);
            if (pastPath != null && pastPath.Count > 0)
            {
                SubPath(pastPath[^1]);
                Reload();
            }
            else
            {
                pastPath = new List<string>();
                Reload(true);
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                goBack();
            }
        }
        bool Is_Pinned = false;

        private void pinImage_Click(object sender, MouseEventArgs e)
        {
            Is_Pinned = !Is_Pinned;
            pinImage.Image = Is_Pinned ? Properties.Resources.unPin : Properties.Resources.pin;
        }
    }
}