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
 
        private string file_name;
        private long right_counter = 0;
        private long fail_counter = 0;
        private long life = 5;
        private string text_save = "";
        const string nameSpace_resorse = "MathTrap.Risorse.";
        private Stream stream ;
        private StreamReader reader;
        private StreamWriter sw;

        public ClassSave(string file_name) {

            this.file_name = file_name;

            this.composedText();
            
            this.stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(nameSpace_resorse + file_name);
            //this.stream = File.OpenWrite(file_name);
            //FileStream a = new FileStream("SaveScore.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //leggi file txt associato alla soluzione
            if (this.stream.CanRead)
                this.reader = new StreamReader(this.getStream());
            
            //scrivi file txt associato alla soluzione
            if (this.stream.CanWrite)
                this.sw = new StreamWriter(this.getStream());
            
                

            
        }

        public void SaveAsync(string value){
            this.getSw().Write(value);
        }

        public void LoadAsync(){
            this.setTextSave(this.getReader().ReadToEnd());
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

        public void CreateDirectory(string filePathDir) {
            if (!File.Exists(filePathDir))
            {
                Directory.CreateDirectory(filePathDir);
            }
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
    }
}
