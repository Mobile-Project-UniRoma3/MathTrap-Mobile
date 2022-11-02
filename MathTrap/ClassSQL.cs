using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;



namespace MathTrap
{
    public class TableItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
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
    public class AsyncLazy<T>
    {

        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }

       
    }
   
    public class ClassSQL 
    {
        static SQLiteAsyncConnection Database;
        
        public const string DatabaseFilename = "MathTrap.db3";

        // specifica i valori enum predefiniti SQLiteOpenFlagutilizzati per inizializzare la connessione al database.
        public const SQLite.SQLiteOpenFlags Flags =
        // La connessione creerà automaticamente il file di database se non esiste.
        SQLite.SQLiteOpenFlags.ReadWrite |
        // La connessione può leggere e scrivere dati.
        SQLite.SQLiteOpenFlags.Create |
        // la connessione parteciperà alla cache condivisa, se abilitata
        SQLite.SQLiteOpenFlags.SharedCache;

        public ClassSave save;

        public static readonly AsyncLazy<ClassSQL> Instance = new AsyncLazy<ClassSQL>(async () => { 
            var instance = new ClassSQL();  CreateTableResult result = await Database.CreateTableAsync<TableItem>();  return instance; 
        });

        public ClassSQL()
        {
            this.save= new ClassSave(3, DatabaseFilename);
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
            EnabledReadWrite();
        }

        async private void EnabledReadWrite() 
        { 
            await Database.EnableWriteAheadLoggingAsync(); 
        }
      
        public string DatabasePath
        {
            get
            {               
                return this.getSave().PathToFile();
            }
        }

        public ClassSave getSave() {
            return this.save;
        }

        /*
         * Metodi di manipolazione dei dati
         * La Classe ClassSQL include metodi per i quattro tipi di manipolazione dei dati: 
         * creare, leggere, modificare ed eliminare. 
         * La libreria SQLite.NET fornisce una semplice mappa relazionale degli oggetti (ORM) 
         * che consente di archiviare e recuperare oggetti senza scrivere istruzioni SQL.
         */

        async public void CreateTable() {
            await Database.CreateTableAsync<TableItem>();
        }

        public Task<List<TableItem>> GetItemsAsync()
        {
            return Database.Table<TableItem>().ToListAsync();
        }

        public Task<List<TableItem>> GetItemsNotDateAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<TableItem>("SELECT * FROM [tabella] WHERE [Date] = ''");
        }

        public Task<TableItem> GetItemAsync(int id)
        {
            return Database.Table<TableItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<TableItem> GetItemLoad()
        {
            return Database.Table<TableItem>().Where(i => i.done == false).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TableItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                this.CreateTable();
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TableItem item)
        {
            return Database.DeleteAsync(item);
        }


        async void OnDelete(TableItem item)
        {
            await DeleteItemAsync(item);          
        }

    }
}
