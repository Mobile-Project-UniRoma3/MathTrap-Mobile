using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        private const string SaveOper = "SaveOperator.text";
        private const string SaveLang = "SaveLanguage.text";
        private ClassSave save;
        public TableItem item;
        private TableOperator oper;
        private TableLanguage lang;
        private string[] operator_;
        private string[,] lenguage_;

        public ClassSQL()
        {
            this.oper = new TableOperator();
            this.item = new TableItem();
            this.lang = new TableLanguage();
            this.save= new ClassSave();
            this.getSave().accessStream(3, DatabaseFilename);
            Connection = new SQLiteAsyncConnection(DatabasePath);
            CreateTable();
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
                return this.getSave().PathToFile();
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

        private void CreateTable() {
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

        private string[] getSetOperatori() {
            
            this.getSave().accessStream(2, SaveOper);           
            return this.operator_;
        }

        async private void CreateOperator() 
        {
            Connection.CreateTableAsync<TableOperator>().Wait();

            var operazioni = await GetSqlAllOperAsync();
            int i;

            if (operazioni.Count == 0)
            {
                i = composed(1);
                operazioni = await GetSqlAllOperAsync();
            }
            else 
            {
                i = operazioni.Count;
            }

            this.operator_= new string[i];
            
            foreach (var o in operazioni)
            {
                //Console.WriteLine($" {o.operatore}");
                this.operator_[i] = o.text;
            
            }          
        }

        public Task<List<TableLanguage>> GetSqlAllLanguageAsync()
        {
            return Connection.QueryAsync<TableLanguage>("SELECT * FROM [TableLenguage] ");
        }

        public void DeleteLenguageAllAsync()
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

        private string[,] getSetLanguage()
        {
            this.getSave().accessStream(2, SaveLang);
            return this.lenguage_;
        }

        async private void CreateLanguage()
        {
            Connection.CreateTableAsync<TableLanguage>().Wait();

            var language = await GetSqlAllLanguageAsync();
            int i;

            if (language.Count == 0)
            {
                i = composed(2);
                language = await GetSqlAllLanguageAsync();
            }
            else
            {
                i = language.Count;
            }

            this.lenguage_ = new string[i,2];

            foreach (var o in language)
            {
                //Console.WriteLine($" {o.operatore}");
                this.lenguage_[i,1] = o.text;
                this.lenguage_[i,2] = o.state;
            }
        }

        public int composed(int index) 
        {
            //Carico il txt
            this.getSave().LoadAsync();

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
