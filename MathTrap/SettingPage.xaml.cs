using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{

    class OperatorBonus { 
        private string bonus;
        public string Bonus   // property
        {
            get { return bonus; }   // get method
            set { bonus = value; }  // set method
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private ClassSQL value;
        private string textSettings;
        private OperatorBonus bonus;

        public SettingPage(ClassSQL value)
        {
            InitializeComponent();

            //assegno la connessione aperta
            this.value = value;
            this.bonus = new OperatorBonus();
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = new List<string>();
            var operazioni = await this.value.GetAllOperAsync();
            foreach (var o in operazioni)
            {
                list.Add(o.text);
            }
            collectionView.ItemsSource = await disposeListOperator();
           
            this.piker_.ItemsSource = list;
            this.piker_.SelectedItem = this.bonus.Bonus;


        } 
        
        private string getTextSetting()
        {
            return this.textSettings;
        }

        private void setTextSetting(string s) {
            this.textSettings = s;
        }

        async private void OnReturn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(1), false);
        }

        private void OnActionChange(object sender, EventArgs e)
        {
            this.bonus.Bonus = this.piker_.SelectedItem.ToString();
        }

        private void OnSave(object sender, EventArgs e)
        {      
            string str = "";
            var collection = collectionView.ItemsSource;
            foreach (var o in collection)
            {
                this.value.settTemp = (TableTempSetting)o;
                if (this.value.settTemp.check==true) 
                { 
                    str=str+this.value.settTemp.text;
                }
            }
            this.value.sett.ID = 0;
            this.value.sett.text = str;
            this.value.sett.bonus = this.piker_.SelectedItem.ToString();
           
        }

        //-->Funzioni
        async private Task<List<string>> scomposed() {
            List<string> l = new List<string>();
            
            //Esiste un recordo di setting per la partita
            var setting_ = await this.value.GetSettingLoad(this.value.score.IdSetting);
            if (setting_!= null) 
            {
                string str;
                
                str = setting_.text;
                int i = str.Length;
                int k = 0;
                //operandi
                while (i>0)
                {
                    l.Add(str.Substring(k,1));
                    i--;
                    k++;
                }
                //bonus
                this.bonus.Bonus = setting_.bonus;
            } 
            else 
            {
                var operazioni = await this.value.GetAllOperAsync();
                foreach (var o in operazioni) {
                    l.Add(o.text);
                }
                this.bonus.Bonus = "x";
            }
           
            return l;
        }

        async private Task<List<TableTempSetting>> disposeListOperator() {
            //azzero la tab temporanea dei settaggi
            this.value.DeleteTempSettAllAsync();

            //evoco i settaggi reali della partita
            var listTempA = new List<string>(await scomposed());

            //inserisco i settaggi in una tab temp
            foreach (var l in listTempA) {
                this.value.settTemp.ID = 0;
                this.value.settTemp.text = l.ToString();
                this.value.settTemp.check = true;
                if (this.bonus.Bonus != null)
                {
                    this.value.settTemp.bonus = this.bonus.Bonus;
                }
                else
                { 
                    this.value.settTemp.bonus = "x";
                }
                this.value.SaveTempSettingAsync(this.value.settTemp);                    
            }

            var operazioni = await this.value.GetAllOperAsync();
            foreach (var o in operazioni)
            {
                if (!listTempA.Contains(o.text)) {
                    this.value.settTemp.ID = 0;
                    this.value.settTemp.text = o.text;
                    this.value.settTemp.check = false;
                    if (this.bonus.Bonus != null)
                    {
                        this.value.settTemp.bonus = this.bonus.Bonus;
                    }
                    else
                    {
                        this.value.settTemp.bonus = "x";
                    }
                    this.value.SaveTempSettingAsync(this.value.settTemp);
                }
               
            }
            var ritorno = await this.value.GetTempSettingAllAsync();
            return ritorno; 
        }
        
    }

   
}