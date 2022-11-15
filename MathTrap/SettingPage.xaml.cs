using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private ClassSQL value;

        public SettingPage(ClassSQL value)
        {
            InitializeComponent();

            //assegno la connessione aperta
            this.value = value;
            
        }
        async private void OnReturn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(1), false);
        }

        async private void OnCheckBoxCheckedChanged_One(object sender, CheckedChangedEventArgs e)
        {
             
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = new List<string>(await scomposed());

            collectionView.ItemsSource = list;
           
            this.piker_.ItemsSource = list; 
             
        }

        private void Save(TableScore score) 
        {
            this.value.SaveScoreAsync(score); 
        }

        async private void OnSave(object sender, EventArgs e)
        {                   
                this.OnAppearing();  
        }

        async private Task<List<string>> scomposed() {
            List<string> l = new List<string>();
            //

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
            } 
            else 
            {
                var operazioni = await this.value.GetAllOperAsync();
                foreach (var o in operazioni) {
                    l.Add(o.text);
                }
            }
           
            return l;
        }

        private void OnActionChange(object sender, EventArgs e)
        {

        }
    }
}