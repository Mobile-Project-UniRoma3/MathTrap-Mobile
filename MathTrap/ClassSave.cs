using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace MathTrap
{

    public class ClassSave
    {
       [assembly : IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)),Assembly]
 
        
        private long right_counter;
        private long fail_counter;
        private long life;

        const string nameSpace_resorse = "MathTrap.Risorse.";
        private string file_name;
        private string text_save;
        
        private Stream stream ;
        private FileStream file;
        private StreamReader reader;
        private StreamWriter sw;

        public ClassSave(string file_name) {
            this.right_counter = 0;
            this.fail_counter = 0;
            this.life = 5;
            this.file_name = file_name;
            this.text_save = "";
            this.composedText();
                     
            // MemoryStream stream = new MemoryStream();
            // this.sw = new StreamWriter(fs);
            //
            //  Stream.CopyTo(stream);
            //  this.sw.Write(stream.ToArray());
            //}
          
        }

        public void Save(string value){
            this.getSw().Write(value);
        }

        public void Load(){
            this.setTextSave(this.getReader().ReadToEnd());
        }

        async public void SaveAsync(string filename, string text)
        {
            string path = this.PathToFile(filename);
            using (this.sw = File.CreateText(path))
                await this.getSw().WriteAsync(text);
        }

        async public void LoadAsync(string filename)
        {
            string path = this.PathToFile(filename);
            using (this.reader = File.OpenText(path))
                this.setTextSave(await this.getReader().ReadToEndAsync());
        }

        public bool FileExists(string filename)
        {
            return (File.Exists(this.PathToFile(filename))) ? false : true;
        }

        public void ClearData(string filename)
        {       
                File.Delete(this.PathToFile(filename));
        } 

        private string PathToFile(string filename)
        {
            return Path.Combine(this.PathToDir(), filename);
        }

        private string PathToDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
     
        private void CreateDirectory(string filePathDir) {
            if (!File.Exists(filePathDir))
            {
                Directory.CreateDirectory(filePathDir);
            }
        }
     
        private void Stream() {
            this.stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.getNameSpace_resorse() + this.getNameFile());
        }

        private void FileStream() { 
            this.file = new FileStream(Path.Combine(this.PathToFile(this.getNameFile())), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        private void StreamRead() { 
            this.reader = new StreamReader(this.getStream());
        }

        private void StreamWtrite() { 
            this.sw = new StreamWriter(this.getStream());
        }

        private bool canRead() {
            return ((this.getStream().CanRead)) ? false : true;
        }

        private bool canWrite() {
            return ((this.getStream().CanWrite)) ? false : true;
        }

        private Stream getStream() {
            return this.stream; 
        }

        private StreamReader getReader() {
            return this.reader;
        }

        private StreamWriter getSw() {
            return this.sw;
        }

        private string getNameSpace_resorse() {
            return nameSpace_resorse;
        }

        public string getNameFile() {
            return this.file_name;
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

        public string getTextSave()
        {
            return this.text_save;
        }

        private void setTextSave(string t)
        {
            this.text_save = t;
        }

        private void composedText() {
            this.setTextSave(Convert.ToString(this.right_counter) + ";"
                                 + Convert.ToString(this.fail_counter) + ";"
                                 + Convert.ToString(this.life));
        }

        public void composedScore() {
            string r;
            string f;
            string l;

            int i = this.getTextSave().Length;
            int k = this.getTextSave().IndexOf(';');
            //La sottostringa inizia in corrispondenza della posizione del carattere specificata e ha la lunghezza specificata
            r = this.getTextSave().Substring(0, k);
            k +=1;
            i = (i - k);
            f = this.getTextSave().Substring(k, i);

            k = f.IndexOf(';');
            l = f;
            f = f.Substring(0, k);
           
            k +=1;
            i = (i - k);
            l = l.Substring(k,i);
            
            this.setRight(Convert.ToInt64(r));
            this.setFail(Convert.ToInt64(f));
            this.setLife(Convert.ToInt64(l));
        }
    }
}
