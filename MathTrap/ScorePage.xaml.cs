using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {

        private ClassSQL value;

        public ScorePage()
        {
            InitializeComponent();

            //assegno la connessione aperta
            value = App.connection;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = await this.value.GetItemsAllAsync();
        }

        async private void onMain(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(), false);
        }

        private void onClear(object sender, EventArgs e)
        {
            DeleteAll();
            OnAppearing();
        }

       private void DeleteAll() {
            this.value.DeleteItemAllAsync();
        }
    }
}
