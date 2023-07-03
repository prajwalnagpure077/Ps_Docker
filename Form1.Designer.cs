namespace PsDocker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            listView1 = new ListView();
            path = new ColumnHeader();
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            hiiToolStripMenuItem = new ToolStripMenuItem();
            homeImage = new PictureBox();
            nameLabel = new Label();
            backImage = new PictureBox();
            DropGraphicsPanel = new PictureBox();
            pinImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)homeImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DropGraphicsPanel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pinImage).BeginInit();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // listView1
            // 
            listView1.Activation = ItemActivation.OneClick;
            listView1.AllowDrop = true;
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.BackColor = Color.FromArgb(137, 137, 137);
            listView1.BorderStyle = BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { path });
            listView1.Location = new Point(0, 21);
            listView1.Name = "listView1";
            listView1.Size = new Size(700, 308);
            listView1.TabIndex = 0;
            listView1.TabStop = false;
            listView1.TileSize = new Size(70, 120);
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.DragDrop += listView1_DragDrop;
            listView1.DragEnter += listView1_DragEnter;
            listView1.DragLeave += listView1_DragLeave;
            listView1.KeyDown += listView1_KeyDown;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // path
            // 
            path.Tag = "path";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(46, 46, 46);
            pictureBox1.Cursor = Cursors.SizeAll;
            pictureBox1.Location = new Point(0, 335);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 64);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { hiiToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(118, 26);
            // 
            // hiiToolStripMenuItem
            // 
            hiiToolStripMenuItem.Name = "hiiToolStripMenuItem";
            hiiToolStripMenuItem.Size = new Size(117, 22);
            hiiToolStripMenuItem.Text = "Remove";
            hiiToolStripMenuItem.Click += hiiToolStripMenuItem_Click;
            // 
            // homeImage
            // 
            homeImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            homeImage.BackColor = Color.FromArgb(46, 46, 46);
            homeImage.Cursor = Cursors.Hand;
            homeImage.Image = Properties.Resources.HomeButton;
            homeImage.Location = new Point(20, 352);
            homeImage.Name = "homeImage";
            homeImage.Size = new Size(30, 30);
            homeImage.SizeMode = PictureBoxSizeMode.Zoom;
            homeImage.TabIndex = 2;
            homeImage.TabStop = false;
            homeImage.MouseClick += homeImage_MouseClick;
            homeImage.MouseEnter += homeImage_MouseEnter;
            homeImage.MouseLeave += homeImage_MouseLeave;
            // 
            // nameLabel
            // 
            nameLabel.AccessibleRole = AccessibleRole.None;
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.FromArgb(46, 46, 46);
            nameLabel.CausesValidation = false;
            nameLabel.Enabled = false;
            nameLabel.Font = new Font("JetBrains Mono", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point);
            nameLabel.ForeColor = Color.White;
            nameLabel.Location = new Point(464, 351);
            nameLabel.Name = "nameLabel";
            nameLabel.RightToLeft = RightToLeft.Yes;
            nameLabel.Size = new Size(224, 31);
            nameLabel.TabIndex = 3;
            nameLabel.Text = "made by Prajwal";
            // 
            // backImage
            // 
            backImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            backImage.BackColor = Color.FromArgb(46, 46, 46);
            backImage.Image = Properties.Resources.back;
            backImage.Location = new Point(56, 352);
            backImage.Name = "backImage";
            backImage.Size = new Size(30, 30);
            backImage.SizeMode = PictureBoxSizeMode.Zoom;
            backImage.TabIndex = 4;
            backImage.TabStop = false;
            backImage.Click += backImage_Click;
            // 
            // DropGraphicsPanel
            // 
            DropGraphicsPanel.BackColor = Color.FromArgb(46, 46, 46);
            DropGraphicsPanel.BackgroundImageLayout = ImageLayout.None;
            DropGraphicsPanel.Dock = DockStyle.Fill;
            DropGraphicsPanel.Enabled = false;
            DropGraphicsPanel.Image = Properties.Resources.LinkPanel;
            DropGraphicsPanel.Location = new Point(0, 0);
            DropGraphicsPanel.Name = "DropGraphicsPanel";
            DropGraphicsPanel.Size = new Size(700, 397);
            DropGraphicsPanel.SizeMode = PictureBoxSizeMode.Zoom;
            DropGraphicsPanel.TabIndex = 5;
            DropGraphicsPanel.TabStop = false;
            DropGraphicsPanel.Visible = false;
            // 
            // pinImage
            // 
            pinImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pinImage.BackColor = Color.FromArgb(46, 46, 46);
            pinImage.Image = Properties.Resources.pin;
            pinImage.Location = new Point(92, 351);
            pinImage.Name = "pinImage";
            pinImage.Size = new Size(30, 30);
            pinImage.SizeMode = PictureBoxSizeMode.Zoom;
            pinImage.TabIndex = 4;
            pinImage.TabStop = false;
            pinImage.MouseDown += pinImage_Click;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(137, 137, 137);
            ClientSize = new Size(700, 397);
            ControlBox = false;
            Controls.Add(pinImage);
            Controls.Add(backImage);
            Controls.Add(nameLabel);
            Controls.Add(homeImage);
            Controls.Add(pictureBox1);
            Controls.Add(listView1);
            Controls.Add(DropGraphicsPanel);
            ForeColor = SystemColors.ControlDarkDark;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "MyDocker";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)homeImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)backImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)DropGraphicsPanel).EndInit();
            ((System.ComponentModel.ISupportInitialize)pinImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ListView listView1;
        private PictureBox pictureBox1;
        private ColumnHeader path;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem hiiToolStripMenuItem;
        private PictureBox homeImage;
        private Label nameLabel;
        private PictureBox backImage;
        private PictureBox DropGraphicsPanel;
        private PictureBox pinImage;
    }
}