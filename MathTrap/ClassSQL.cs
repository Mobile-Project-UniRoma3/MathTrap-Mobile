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
        public int CONST_OPERATOR = 3;
        private File save;

        public TableItem item;
        public TableOperator oper;
        public TableLanguage lang;

        private string[,] operator_;
        

        public ClassSQL()
        {
            this.oper = new TableOperator();
            this.item = new TableItem();
            this.lang = new TableLanguage();
            this.save= new File();
            
            Connection = new SQLiteAsyncConnection(DatabasePath);

            //DeleteItemAllAsync();
            //DeleteOperAllAsync();
            //DeleteLangAllAsync();
        }
     
        private string DatabasePath
        {
            get
            {               
                return this.getSave().PathToFile(this.getSave().PathToDirApplicationData(), DatabaseFilename);
            }
        }

        // --> GET & SET
        private File getSave() {
            return this.save;
        }

        public TableItem getItem
        {
            get { return this.item; }
        }

        public TableLanguage getLang
        {
            get { return this.lang; }
        }

        public string[,] getOperandi() 
        {              
            return this.operator_;
        }

        public void setOperandi(string[,] s)
        {
            this.operator_ = s;
        }

        /*
        * Metodi di manipolazione dei dati
        * La Classe ClassSQL include metodi per i quattro tipi di manipolazione dei dati: 
        * creare, leggere, modificare ed eliminare. 
        * La libreria SQLite.NET fornisce una semplice mappa relazionale degli oggetti (ORM) 
        * che consente di archiviare e recuperare oggetti senza scrivere istruzioni SQL.
        */

        //--> SQL Punteggio
        public void CreateItem() {
            Connection.CreateTableAsync<TableItem>().Wait();
        }

        async public Task<List<TableItem>> GetItemsAllAsync()
        {
            return await Connection.Table<TableItem>().ToListAsync();
        }

        async public Task<List<TableItem>> GetSqlItemsAllAsync()
        {
            return await Connection.QueryAsync<TableItem>("SELECT * FROM [TableItem] ");
        }

        async public Task<TableItem> GetItemWhereIdAsync(int id)
        {
            return await Connection.Table<TableItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<TableItem> GetItemLoad()
        {
            return Connection .Table<TableItem>().Where(i => i.done == false).FirstOrDefaultAsync();
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

        //-->SQL Operandi 
        async public void CreateOperator() 
        {
            Connection.CreateTableAsync<TableOperator>().Wait();
            var operazioni = await GetAllOperAsync();
            int i= operazioni.Count;
            string flag_oper = "1";
            string flag_bonus = "0";

            if ((operazioni.Count) ==0)
            {
                composed(1, SaveOper);              
            }
           
            operazioni =await GetAllOperAsync();
            i = operazioni.Count;

            this.operator_= new string[i,3];
            i = 0;
            foreach (var o in operazioni)
            {
                //i primi 4 operandi sono di default true
                this.operator_[i, 0] = o.text;
                if (i > 3) {
                    flag_oper = "0";
                }
                this.operator_[i, 1] = flag_oper;

                if (o.text == "x")
                {
                    this.operator_[i, 2] = "1";//-->operatore bonus di default 
                }
                else 
                { 
                    this.operator_[i, 2] = flag_bonus;
                }
                
                i++;
            }          
        }

        async public Task<List<TableOperator>> GetSqlAllOperAsync()
        {
            return await Connection.QueryAsync<TableOperator>("SELECT * FROM [TableOperator] ");         
        }

        async public Task<List<TableOperator>> GetAllOperAsync()
        {
            return await Connection.Table<TableOperator>().ToListAsync();
        }

        public void DeleteOperAllAsync()
        {
            Connection.DeleteAllAsync<TableOperator>().Wait();
        }

        async public Task<TableOperator> GetOperLoad(string text)
        {
            return await Connection.Table<TableOperator>().Where(i => i.text == text).FirstOrDefaultAsync();
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

        //--> SQL linguaggi
        async public void CreateLanguage()
        {
            Connection.CreateTableAsync<TableLanguage>().Wait();

            var language = await GetAllLanguageAsync();
            int i= language.Count;
            
            if (language.Count == 0)
            {
                composed(2, SaveLang);   
            }
            string l = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            this.lang = await GetLenguageLoad(l);           
        }

        async public Task<List<TableLanguage>> GetSqlAllLanguageAsync()
        {
            return await Connection.QueryAsync<TableLanguage>("SELECT * FROM [TableLanguage] ");
        }

        async public Task<List<TableLanguage>> GetAllLanguageAsync()
        {
            return await Connection.Table<TableLanguage>().ToListAsync(); 
        }


        public void DeleteLangAllAsync()
        {
            Connection.DeleteAllAsync<TableLanguage>().Wait();
        }

        async public Task<TableLanguage> GetLenguageLoad(string state)
        {
            return await Connection.Table<TableLanguage>().Where(i => i.state == state).FirstOrDefaultAsync();
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

        //-->Funzioni di composizione
        public void composed(int index, string file) 
        {
            //Carico il txt
            string str;
            str = this.getSave().ApriFile(file);

            string r;

            //lunghezza del testo
            int i = str.Length;
            int k = 0;
            do
            {
                k = str.IndexOf('§');
                // Ritorna la sotto stringa di lunghezza k dalla posizione 0  
                r = str.Substring(0, k);
                insertIntoDateComposed(index, r);
                // Salto alla posizione successiva
                k = k + 1;
                // Calcolo la lunghezza residua
                i = i - k;
                // Ritorna la sotto stringa di lunghezza k dalla posizione 0
                str = str.Substring(k, i);
            } while (str.IndexOf('§') >= 0);
            r = str;
            insertIntoDateComposed(index, r);
        }

        public void insertIntoDateComposed(int index, string str) {
            switch (index) {
                case 1 : this.oper.ID = 0; this.oper.text = str;
                         Connection.InsertAsync(this.oper).Wait();
                         break;

                case 2: this.lang.ID = 0; this.lang.text = str.Substring(2, (str.Length -2)); this.lang.state= str.Substring(0, 2);
                        Connection.InsertAsync(this.lang).Wait(); 
                        break;
                
                default:break;
            }

        }

    }

}
