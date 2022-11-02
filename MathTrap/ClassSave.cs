using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MathTrap
{
    
    public class ClassSave
    {
        [assembly: Dependency(typeof(IFileManager_OSName))]
        [assembly: IntrospectionExtensions.GetTypeInfo(typeof(LoadResourceText)), Assembly] 

   
        private int id_score;
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
       

        public ClassSave(int index, string file_name) {
            this.right_counter = 0;
            this.fail_counter = 0;
            this.life = 5;
            this.file_name = file_name;
            this.text_save = "";
            this.composedText();

            switch (index) {
                case 2 :this.setStream();
                       break;

                default:this.setFileStream();
                        this.stream = this.getFileStream();
                        break;           
            }
                  
            this.setStreamRead(this.getStream());
            this.setStreamWrite(this.getStream());     
        }
       
        public void Save(string value)
        {
            this.CreateFile();
            this.getSw().Write(value);
        }

        public void Load(){
            this.setTextSave(this.getReader().ReadToEnd());
        }

        async public void SaveAsync(string text)
        {
            this.CreateFile();
            //using (this.sw = File.CreateText(this.PathToFile(this.getNameFile())))
            await this.getSw().WriteAsync(text);
        }

        async public void LoadAsync()
        {
            //using (this.reader = File.OpenText(this.PathToFile(this.getNameFile())))
            this.setTextSave(await this.getReader().ReadToEndAsync());
        }

        public bool FileExists()
        {
            return (File.Exists(this.PathToFile())) ? false : true;
        }

        public void ClearData()
        {       
                File.Delete(this.PathToFile());
        }

        public string PathToFile()
        {
            return Path.Combine(this.PathToDir(), this.getNameFile());
        }

        public string PathToDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
     
        private void CreateDirectory() {
            if (!File.Exists(this.PathToDir()))
            {
                Directory.CreateDirectory(this.PathToDir());
            }
        }

        private void CreateFile()
        {
            if (!this.FileExists())
            {
                this.CreateDirectory();
                File.Create(this.PathToFile());
            }
        }

        private void setStream() {
            this.stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.getNameSpace_resorse() + this.getNameFile());
        }

        private void setFileStream() { 
            this.file = new FileStream(Path.Combine(this.PathToFile()), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        private void setStreamRead(Stream getStream) { 
            this.reader = new StreamReader(getStream);
        }

        private void setStreamWrite(Stream getStream) { 
            this.sw = new StreamWriter(getStream);
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

        private FileStream getFileStream()
        {
            return this.file;
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

        public int getId()
        {
            return this.id_score;
        }

        public void setId(int v)
        {
            this.id_score = v;
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

        public void composedScore(TableItem item)
        {
            int i=0;
            long r=0;
            long f=0;
            long l=5;

            if (item.ID >0) {
                i = item.ID;
                r = item.right;
                f = item.fail;
                l = item.life;
            }

            this.setId(i);
            this.setRight(r);
            this.setFail(f);
            this.setLife(l);
        }
    }
}
