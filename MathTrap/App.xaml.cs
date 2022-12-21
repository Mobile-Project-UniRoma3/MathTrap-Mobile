using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    public partial class App : Application
    {
        /*
         * La connessione va aperta una volta sola
         * altrimrnti genera un'eccezione.
         */
        static ClassSQL database;

        
        public static ClassSQL connection {
            get
            {
                if (database == null)
                {
                    database = new ClassSQL();
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            connection.CreateScore();

            connection.CreateSetting();

            connection.CreateTempSetting();

            connection.CreateOperator();

            connection.CreateLanguage();

            MainPage = new MainPage();          
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
