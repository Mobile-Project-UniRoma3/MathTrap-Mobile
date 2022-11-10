using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Globalization;
using SQLite;



namespace MathTrap
{
    public class TableOperator
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string text { get; set; }
    }

    public class TableLanguage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string text { get; set; }
        public string state { get; set; }
    }

    public class TableItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public bool flag_p { get; set; }
        public bool flag_r { get; set; }
        public string bonus { get; set; }
        public long right { get; set; }
        public long fail { get; set; }
        public long life { get; set; }
        public string date { get; set; }
        public bool done { get; set; }
    }

    /*
     * Per avviare l'inizializzazione del database, evitare di bloccare l'esecuzione 
     * e avere l'opportunità di rilevare le eccezioni, l'applicazione di esempio 
     * utilizza l'inizializzazione lazy asincrona. 
     * Il delegato factory passato al costruttore può essere sincrono o asincrono.
     */
    
   
    public class ClassSQL 
    {
        static SQLiteAsyncConnection Connection;
        
        private const string DatabaseFilename = "MathTrap.db3";
        private const string SaveOper = "SaveOperator.txt";
        private const string SaveLang = "SaveLanguage.txt";

        private ClassSave save;

        public TableItem item;
        public TableOperator oper;
        public TableLanguage lang;

        private string[,] operator_;
        

        public ClassSQL()
        {
            this.oper = new TableOperator();
            this.item = new TableItem();
            this.lang = new TableLanguage();
            this.save= new ClassSave();
            
            Connection = new SQLiteAsyncConnection(DatabasePath);

            DeleteItemAllAsync();
            DeleteOperAllAsync();
            DeleteLangAllAsync();


            CreateItem();
            
            CreateOperator();
            
            CreateLanguage();
        }

        async private void EnabledReadWrite() 
        { 
            await Connection.EnableWriteAheadLoggingAsync(); 
        }
      
        private string DatabasePath
        {
            get
            {               
                return this.getSave().PathToFile(this.getSave().PathToDirApplicationData(), DatabaseFilename);
            }
        }

        private ClassSave getSave() {
            return this.save;
        }

        public TableItem getItem
        {
            get { return this.item; }
        }
        /*
        * Metodi di manipolazione dei dati
        * La Classe ClassSQL include metodi per i quattro tipi di manipolazione dei dati: 
        * creare, leggere, modificare ed eliminare. 
        * La libreria SQLite.NET fornisce una semplice mappa relazionale degli oggetti (ORM) 
        * che consente di archiviare e recuperare oggetti senza scrivere istruzioni SQL.
        */

        private void CreateItem() {
            Connection.CreateTableAsync<TableItem>().Wait();
        }

        public Task<List<TableItem>> GetItemsAllAsync()
        {
            return Connection.Table<TableItem>().ToListAsync();
        }

        public Task<List<TableItem>> GetSqlItemsAllAsync()
        {
            return Connection.QueryAsync<TableItem>("SELECT * FROM [TableItem] ");
        }

        public Task<TableItem> GetItemWhereIdAsync(int id)
        {
            return Connection.Table<TableItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<TableItem> GetItemLoad()
        {
            return Connection.Table<TableItem>().Where(i => i.done == false).FirstOrDefaultAsync();
        }

        public void SaveItemAsync(TableItem item)
        {
            if (item.ID != 0)
            {
                Connection.UpdateAsync(item).Wait();
            }
            else
            {
                Connection.InsertAsync(item).Wait();
            }
        }

        public void DeleteItemAsync(TableItem item)
        {
            Connection.DeleteAsync(item).Wait();
        }

        public void DeleteItemAllAsync()
        {
            Connection.DeleteAllAsync<TableItem>().Wait();
        }

        public Task<List<TableOperator>> GetSqlAllOperAsync()
        {
            return Connection.QueryAsync<TableOperator>("SELECT * FROM [TableOperator] ");         
        }

        public Task<List<TableOperator>> GetAllOperAsync()
        {
            return Connection.Table<TableOperator>().ToListAsync();
        }

        public void DeleteOperAllAsync()
        {
            Connection.DeleteAllAsync<TableOperator>().Wait();
        }

        public Task<TableOperator> GetOperLoad(string text)
        {
            return Connection.Table<TableOperator>().Where(i => i.text == text).FirstOrDefaultAsync();
        }

        public void SaveOperAsync(TableOperator item)
        {
            if (item.ID != 0)
            {
                Connection.UpdateAsync(item).Wait();
            }
            else
            {
                Connection.InsertAsync(item).Wait();
            }
        }

        public string[,] getOperandi() 
        {              
            return this.operator_;
        }

        async private void CreateOperator() 
        {
            Connection.CreateTableAsync<TableOperator>().Wait();
            var operazioni = await GetAllOperAsync();
            int i= operazioni.Count;

            
            if ((operazioni.Count) ==0)
            {
                composed(1, SaveOper);              
            }
           
            operazioni =await GetAllOperAsync();
            i = operazioni.Count;

            this.operator_= new string[i,3];
            
            foreach (var o in operazioni)
            {
                //Console.WriteLine($" {o.operatore}");
                this.operator_[i, 1] = o.text;
                this.operator_[i, 2] = "0";
                this.operator_[i, 3] = "0";
            }          
        }

        public Task<List<TableLanguage>> GetSqlAllLanguageAsync()
        {
            return Connection.QueryAsync<TableLanguage>("SELECT * FROM [TableLanguage] ");
        }

        public Task<List<TableLanguage>> GetAllLanguageAsync()
        {
            return Connection.Table<TableLanguage>().ToListAsync();
        }


        public void DeleteLangAllAsync()
        {
            Connection.DeleteAllAsync<TableLanguage>().Wait();
        }

        public Task<TableLanguage> GetLenguageLoad(string state)
        {
            return Connection.Table<TableLanguage>().Where(i => i.state == state).FirstOrDefaultAsync();
        }

        public void SaveLangAsync(TableLanguage item)
        {
            if (item.ID != 0)
            {
                Connection.UpdateAsync(item).Wait();
            }
            else
            {
                Connection.InsertAsync(item).Wait();
            }
        }

        async private void CreateLanguage()
        {
            Connection.CreateTableAsync<TableLanguage>().Wait();

            var language = await GetAllLanguageAsync();
            int i= language.Count;

            if (language.Count == 0)
            {
                composed(2, SaveLang);
                this.lang = await GetLenguageLoad(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }
            
        }

        public int composed(int index, string file) 
        {
            //Carico il txt
            if(this.getSave().getStream()!=null)
                this.getSave().getStream().Close();
            if (this.getSave().getReader() != null)
                this.getSave().getReader().Close();

            this.getSave().setStream(this.getSave().AssigneStream(this.getSave().getNameSpace_resorse(), file));
            this.getSave().setTextSave(this.getSave().StreamRead(this.getSave().getStream())) ;
            
         
            int j = 0;
            
            string r;
            string f;
            
            //lunghezza del testo
            int i = this.getSave().getTextSave().Length;
            int k = 0; 

            k = this.getSave().getTextSave().IndexOf('§'); 
            r = this.getSave().getTextSave().Substring(0, k);

            insertIntoDateComposed(index, r);

            f = this.getSave().getTextSave().Substring(k, i);
            
            for (j = 1; f.IndexOf('§') >= 0; j++){
                     
                i = (i - k);

                k = f.IndexOf('§');

                r = f.Substring(1, k);
                insertIntoDateComposed(index, r);

                f = f.Substring(k, i);

            }
            return j;
        }


        public void insertIntoDateComposed(int index, string str) {
            switch (index) {
                case 1 : this.oper.ID = 0; this.oper.text = str;
                         Connection.InsertAsync(this.oper).Wait();
                         break;

                case 2: this.lang.ID = 0; this.lang.text = str.Substring(2, str.Length); this.lang.state= str.Substring(0, 2);
                        Connection.InsertAsync(this.lang).Wait(); 
                        break;
                
                default:break;
            }

        }




    }

}
