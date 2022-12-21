using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Globalization;
using SQLite;
using SQLitePCL;
using System.Runtime.InteropServices;
using Xamarin.Forms.Shapes;

namespace MathTrap
{

    public class TableTempSetting
    {
        //in questo caso a ogni operando corrisponde un record
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string text { get; set; }
        public bool check { get; set; }
        public string bonus { get; set; }
    }
    
    public class TableSetting
    {
        //in questo caso tutti gli operandi vengono assemblati in una stringa
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string text { get; set; }
        public string bonus { get; set; }
    }

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

    public class TableScore
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }   
        public long right { get; set; }
        public long fail { get; set; }
        public long life { get; set; }
        public string date { get; set; }
        public bool done { get; set; }
        public int IdSetting { get; set; }
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
        public TableScore score;
        public TableOperator oper;
        public TableLanguage lang;
        public TableSetting sett;
        public TableTempSetting settTemp;

        private string[,] operator_;


        public ClassSQL()
        {   
            SQLitePCL.Batteries.Init();
            this.oper = new TableOperator();
            this.score = new TableScore();
            this.lang = new TableLanguage();
            this.sett = new TableSetting();
            this.settTemp = new TableTempSetting();
            this.save = new File();
            
             
            Connection = new SQLiteAsyncConnection(DatabasePath); 
           
            //DeleteAll();
        }

        private string DatabasePath
        {
            get
            {
                return this.getSave().PathToFile(this.getSave().PathToDirApplicationData(), DatabaseFilename);
            }
        }

        // --> GET & SET
        private File getSave()
        {
            return this.save;
        }

        public TableScore getScore
        {
            get { return this.score; }
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

        public void DeleteAll()
        {
            DeleteScoreAllAsync();
            DeleteOperAllAsync();
            DeleteLangAllAsync();
            DeleteSettAllAsync();
            DeleteTempSettAllAsync();
        }

        /*
        * Metodi di manipolazione dei dati
        * La Classe ClassSQL include metodi per i quattro tipi di manipolazione dei dati: 
        * creare, leggere, modificare ed eliminare. 
        * La libreria SQLite.NET fornisce una semplice mappa relazionale degli oggetti (ORM) 
        * che consente di archiviare e recuperare oggetti senza scrivere istruzioni SQL.
        */

        //--> SQL Punteggio
        public void CreateScore()
        {          
            Connection.CreateTableAsync<TableScore>().Wait() ;
        }

        async public Task<List<TableScore>> GetScoreAllAsync()
        {
            return await Connection.Table<TableScore>().ToListAsync();
        }

        async public Task<List<TableScore>> GetSqlScoreAllAsync()
        {
            return await Connection.QueryAsync<TableScore>("SELECT * FROM [TableScore] ");
        }

        async public Task<TableScore> GetScoreWhereIdAsync(int id)
        {
            return await Connection.Table<TableScore>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<TableScore> GetScoreLoad()
        {
            return Connection.Table<TableScore>().Where(i => i.done == false).FirstOrDefaultAsync();
        }

        public void SaveScoreAsync(TableScore item)
        {
            if (score.ID != 0)
            {
                Connection.UpdateAsync(item).Wait();
            }
            else
            {
                Connection.InsertAsync(item).Wait();
            }
        }

        public void DeleteItemAsync(TableScore score)
        {
            Connection.DeleteAsync(score).Wait();
        }

        public void DeleteScoreAllAsync()
        {
            Connection.DeleteAllAsync<TableScore>().Wait();
        }

        //-->SQL Operandi 
        async public void CreateOperator()
        {
            Connection.CreateTableAsync<TableOperator>().Wait();
            var operazioni = await GetAllOperAsync();

            if ((operazioni.Count) == 0)
            {
                composed(1, SaveOper);
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

        //--> SQL linguaggi
        async public void CreateLanguage()
        {
            Connection.CreateTableAsync<TableLanguage>().Wait();

            var language = await GetAllLanguageAsync();

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

        //-->SQL Setting
        public void CreateSetting()
        {
           Connection.CreateTableAsync<TableSetting>().Wait();
        }

        async public Task<List<TableSetting>> GetAllSettingAsync()
        {
            return await Connection.Table<TableSetting>().ToListAsync();
        }
        public void DeleteSettAllAsync()
        {
            Connection.DeleteAllAsync<TableSetting>().Wait();
        }

        public void DeleteSettAsync(TableSetting score)
        {
            Connection.DeleteAsync(score).Wait();
        }

        async public Task<TableSetting> GetSettingLoad(int id)
        {
            return await Connection.Table<TableSetting>().Where(i => i.ID == id).FirstOrDefaultAsync(); 
        }

        async public Task<TableSetting> GetSqlSettingIDAsync()
        {
            return await Connection.Table<TableSetting>().OrderByDescending(i => i.ID).FirstAsync();
        }

        public void SaveSettAsync(TableSetting item)
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

        //--> SQL Temp Setting
        public void CreateTempSetting()
        {
            Connection.CreateTableAsync<TableTempSetting>().Wait();
        }

        async public Task<List<TableTempSetting>> GetTempSettingAllAsync()
        {
            return await Connection.Table<TableTempSetting>().ToListAsync();
        }

        public void DeleteTempSettAllAsync()
        {
            Connection.DeleteAllAsync<TableTempSetting>().Wait();
        }

        public void SaveTempSettingAsync(TableTempSetting item)
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

        public void insertIntoDateComposed(int index, string str)
        {
            switch (index)
            {
                case 1:
                    this.oper.ID = 0; this.oper.text = str;
                    Connection.InsertAsync(this.oper).Wait();
                    break;

                case 2:
                    this.lang.ID = 0; this.lang.text = str.Substring(2, (str.Length - 2)); this.lang.state = str.Substring(0, 2);
                    Connection.InsertAsync(this.lang).Wait();
                    break;

                default: break;
            }

        }

        public async Task getTaskDelayAsync(int index)
        {
            await Task.Delay(index);
        }

    }    

}
