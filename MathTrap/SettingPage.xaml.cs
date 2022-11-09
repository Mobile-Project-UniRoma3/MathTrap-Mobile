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
            this.onSave(this.value.item);
        }
        async private void onNew(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(1), false);
        }

        async private void OnCheckBoxCheckedChanged_One(object sender, CheckedChangedEventArgs e)
        {
             
        }

        async private void OnCheckBoxCheckedChanged_Two(object sender, CheckedChangedEventArgs e)
        {
             
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = (System.Collections.IEnumerable)await this.value.GetItemWhereIdAsync(this.value.item.ID);
        }

        private void onSave(TableItem item) 
        {
            this.value.SaveItemAsync(item); 
        }

        async private void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(name_Entry.Text))
            {
                this.value.item.name = name_Entry.Text;
                this.value.item.flag_p = this.flag_p_Entry.IsChecked;
                this.value.item.flag_r = this.flag_r_Entry.IsChecked;
                //this.value.item.bonus=this.

                this.name_Entry.Text = string.Empty;
                this.onSave(this.value.item);
                this.OnAppearing();
            }
        }

    }
}