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
    
   
    public class ClassSQL 
    {
        static SQLiteAsyncConnection Connection;    
        private const string DatabaseFilename = "MathTrap.db3";
        private ClassSave save;
        public TableItem item;


        public ClassSQL()
        {
            this.item = new TableItem();
            this.save= new ClassSave();
            this.getSave().accessStream(3, DatabaseFilename);
            Connection = new SQLiteAsyncConnection(DatabasePath);
            CreateTable();
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

        public ClassSave getSave() {
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
            // SQL queries are also possible
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

        public Task<int> SaveItemAsync(TableItem item)
        {
            if (item.ID != 0)
            {
                return Connection.UpdateAsync(item);
            }
            else
            {
                return Connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TableItem item)
        {
            return Connection.DeleteAsync(item);
        }     

    }
}
