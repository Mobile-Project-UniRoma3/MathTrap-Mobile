using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MathTrap
{

    public class ClassSave
    {
        [assembly: Dependency(typeof(IFileManager_OSName))]

        private const string file_name = "SaveScore.txt";

        async public void SaveAsync(string filename, string text)
        {
            string path = CreatePathToFile(filename);
            using (StreamWriter sw = File.CreateText(path))
                await sw.WriteAsync(text);
        }

        async public void LoadAsync(string filename)
        {
            string path = CreatePathToFile(filename);
            using (StreamReader sr = File.OpenText(path))
                await sr.ReadToEndAsync();
        }

        public bool FileExists(string filename)
        {
            return (File.Exists(CreatePathToFile(filename))) ? false : true;
        }

        public string CreatePathToFile(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }

        public void ClearData(string filename)
        {
            if (FileExists(filename))
            {
                File.Delete(CreatePathToFile(filename));
            }
        }

        public string getNameFile() {
            return file_name;
        }
    }
}
