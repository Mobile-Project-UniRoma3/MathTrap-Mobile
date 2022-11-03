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
            
            this.saveMetodo.item.ID = this.saveMetodo.getSave().getId();
            this.saveMetodo.item.right = this.saveMetodo.getSave().getRight();
            this.saveMetodo.item.fail = this.saveMetodo.getSave().getFail();
            this.saveMetodo.item.life = this.saveMetodo.getSave().getLife();
            this.saveMetodo.item.done = false;
            await this.saveMetodo.SaveItemAsync(this.saveMetodo.getItem);

        }

        async private void comeBackHome() {
            await Navigation.PushModalAsync(new MainPage(), false);
        }

    }
}