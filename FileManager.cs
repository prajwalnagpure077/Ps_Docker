namespace PsDocker
{
    internal static class FileManager
    {
        public static void SaveTxt(string key, string data)
        {
            var filePath = Environment.SpecialFolder.ApplicationData + @"\Data\";
            Directory.CreateDirectory(filePath);
            File.WriteAllText(filePath + key + ".data", data);
        }

        public static string LoadTxt(string key)
        {
            string filePath = Environment.SpecialFolder.ApplicationData + @"\Data\" + key + ".data";
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);
            else
                return null;
        }
    }
}
