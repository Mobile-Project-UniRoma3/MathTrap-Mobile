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
        private long right_counter = 0;
        private long fail_counter = 0;
        private long life = 5;
        private string text_save = "";

        public ClassSave() {         
            this.composedText();
        }

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
            this.setTextSave(await sr.ReadToEndAsync());
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

        public void composedScore() {
            string r;
            string f;
            string l;

            int i = this.getTextSave().Length;
            int k = this.getTextSave().IndexOf(';');
            
            r = this.getTextSave().Substring(0, k);
            f = this.getTextSave().Substring(k + 1, i - 1);
            k = f.IndexOf(';');
            
            f = f.Substring(0, k);
            l = this.getTextSave().Substring(k + 1, i - 1);
            
            this.setRight(Convert.ToInt64(r));
            this.setFail(Convert.ToInt64(f));
            this.setLife(Convert.ToInt64(l));
        }

        public string getNameFile() {
            return file_name;
        }

        public long getRight()
        {
            return this.right_counter;
        }

        public void setRight(long v)
        {
            this.right_counter = v;
        }

        public long getFail()
        {
            return this.fail_counter;
        }

        public void setFail(long v)
        {
            this.fail_counter = v;
        }

        public long getLife()
        {
            return this.life;
        }

        public void setLife(long v)
        {
            this.life = v;
        }

        private string getTextSave()
        {
            return this.text_save;
        }

        private void setTextSave(string t)
        {
            this.text_save = t;
        }

        public void composedText() {
            this.setTextSave(Convert.ToString(this.right_counter) + ";"
                                 + Convert.ToString(this.fail_counter) + ";"
                                 + Convert.ToString(this.life));
        }
    }
}
