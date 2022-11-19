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
        private OperatorBonus bonus;
        private Setting set;

        public SettingPage(ClassSQL value, Setting set)
        {
            InitializeComponent();

            //assegno la connessione aperta
            this.value = value;
            this.set = set;
            this.bonus = this.set.getBonus();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = new List<string>(await this.set.getListOperator());

            collectionView.ItemsSource = await this.set.disposeListOperator();

            this.piker_.ItemsSource = list;
            this.piker_.SelectedItem = this.bonus.Bonus;
        }

        async private void OnReturn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(1, this.set), false);
        }

        private void OnActionChange(object sender, EventArgs e)
        {
            this.bonus.Bonus = this.piker_.SelectedItem.ToString();
        }

        async private void OnSave(object sender, EventArgs e)
        {
            string str = "";
            var collection = collectionView.ItemsSource;
            foreach (var o in collection)
            {
                this.value.settTemp = (TableTempSetting)o;
                if (this.value.settTemp.check == true)
                {
                    str = str + this.value.settTemp.text;
                }
            }
            //--> se IDSetting =0 nuovo isert altrimenti <>0 update
            this.value.sett.ID = this.value.score.IdSetting;
            this.value.sett.text = str;
            this.value.sett.bonus = this.piker_.SelectedItem.ToString();
            //salva sett
            this.value.SaveSettAsync(this.value.sett);

            if (this.value.score.IdSetting == 0)
            {
                this.value.sett = await this.value.GetSqlSettingIDAsync();
            }
            else 
            {
                this.value.sett = await this.value.GetSettingLoad(this.value.score.IdSetting);
            }
            //assegna
            this.value.score.IdSetting = this.value.sett.ID;
            //salva score
            this.value.SaveScoreAsync(this.value.score);
        }
    }
}