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
            collectionView.ItemsSource = (System.Collections.IEnumerable)await this.value.GetAllOperAsync(); //await this.value.GetItemWhereIdAsync(this.value.item.ID);
        }

        private void Save(TableItem item) 
        {
            this.value.SaveItemAsync(item); 
        }

        async private void OnSave(object sender, EventArgs e)
        {                   
                this.OnAppearing();  
        }

    }
}