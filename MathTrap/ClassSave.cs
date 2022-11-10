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

        const string nameSpace_resorse = "MathTrap.Risorse.";
        
        private string text_save;
        
        private Stream stream ;
        private FileStream file;
        private StreamReader reader;
        private StreamWriter sw;
       

        public ClassSave() {}
    
        public void Save(string pathToDir, string file, string value)
        {
            this.CreateFile(pathToDir, file);
            this.getSw().Write(value);
        }

        public void Load(){
            this.setTextSave(this.getReader().ReadToEnd());
        }

        async public void SaveAsync(string pathToDir, string file, string text)
        {
            this.CreateFile(pathToDir, file);
            //using (this.sw = File.CreateText(this.PathToFile(this.getNameFile())))
            await this.getSw().WriteAsync(text);
        }

        async public void LoadAsync()
        {
            //using (this.reader = File.OpenText(this.PathToFile(this.getNameFile())))
            this.setTextSave(await this.getReader().ReadToEndAsync());
        }

        public bool FileExists(string pathToFile)
        {
            return (File.Exists(pathToFile)) ? false : true;
        }

        public void FileDelete(string pathToFile)
        {       
                File.Delete(pathToFile);
        }

        public string PathToFile(string pathToDir, string file)
        {
            return Path.Combine(pathToDir, file);
        }

        public string PathToDirApplicationData()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string PathToDirPersonalFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public void CreateDirectory(string pathToDir) 
        {   
            Directory.CreateDirectory(pathToDir);  
        }

        public void CreateFile(string pathToDir, string file)
        {
            if (!this.FileExists(PathToFile(pathToDir, file)))
            {
                this.CreateDirectory(pathToDir);
                File.Create(PathToFile(pathToDir, file));
            }
        }

        public void setStream(Stream s) {
            this.stream = s; 
        }

        public void setFileStrime(FileStream f) {
            this.file = f;
        }

        public Stream getStream()
        {
            return this.stream;
        }

        public FileStream getFileStream()
        {
            return this.file;
        }

        public StreamReader getReader() {
            return this.reader;
        }

        public StreamWriter getSw() {
            return this.sw;
        }
        public Stream AssigneStream(string nameSpace, string file) {
            Stream assembly = Assembly.GetExecutingAssembly().GetManifestResourceStream(nameSpace + file);
            using (var resourceStream = assembly)
            {
                if (resourceStream == null) 
                    return null;
                else           
                    return resourceStream;              
            }           
        }

        public FileStream AssigneFileStream(string pathToDir, string file) { 
            return new FileStream(Path.Combine(PathToFile(pathToDir, file)), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
       
        public string StreamRead(Stream getStream) 
        {
            string result = "";
            //StreamReader reader = new StreamReader(getStream);
            using (this.reader = new StreamReader(getStream))
            {
                result = reader.ReadToEnd();
            }
            return result;              
        }

        public void setStreamWrite(Stream getStream) { 
            this.sw = new StreamWriter(getStream);
        }

        public bool canRead() {
            return ((this.getStream().CanRead)) ? false : true;
        }

        public bool canWrite() {
            return ((this.getStream().CanWrite)) ? false : true;
        }

        public string getNameSpace_resorse() {
            return nameSpace_resorse;
        }

        public string getTextSave()
        {
            return this.text_save;
        }

        public void setTextSave(string t)
        {
            this.text_save = t;
        }
    
    }
}
