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
    
    public partial class SavePage : ContentPage
    {
        private ClassSQL saveMetodo;
       

        public SavePage(ClassSQL value)
        {
            InitializeComponent(); 
            this.saveMetodo = value;         
        }

        private void onYes(object sender, EventArgs e)
        {
            this.OnSave();
            comeBackHome();
        }

        private void onNo(object sender, EventArgs e)
        {
            comeBackHome();
        }

        async void OnSave()
        {
            TableItem item = new TableItem();
            item.ID = this.saveMetodo.getSave().getId(); 
            item.right = this.saveMetodo.getSave().getRight();
            item.fail = this.saveMetodo.getSave().getFail();
            item.life = this.saveMetodo.getSave().getLife();
            item.done = false;
            await this.saveMetodo.SaveItemAsync(item);

        }

        async private void comeBackHome() {
            await Navigation.PushModalAsync(new MainPage(), false);
        }

    }
}