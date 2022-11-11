using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace MathTrap
{

    public class File
    {
        
        const string nameSpace_resorse = "MathTrap.Risorse.";

        public string ApriFile(string str)
        {
            string s = "";
            Stream assembly = Assembly.GetExecutingAssembly().GetManifestResourceStream(getNameSpace_resorse() + str);
            using (var resourceStream = assembly)
            {
                if (resourceStream != null)

                    using (StreamReader reader = new StreamReader(assembly))
                    {
                        s = reader.ReadToEnd();
                        assembly.Close();
                        reader.Close();
                        return s;
                    }
            }
            return s;
        }

        public bool FileExists(string pathToFile)
        {
            return (System.IO.File.Exists(pathToFile)) ? false : true;
        }

        public void FileDelete(string pathToFile)
        {
            System.IO.File.Delete(pathToFile);
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

        public string getNameSpace_resorse() {
            return nameSpace_resorse;
        }

            
    }
}
