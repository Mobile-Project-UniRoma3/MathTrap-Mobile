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

        private void OnSave()
        {        
            this.saveMetodo.SaveScoreAsync(this.saveMetodo.getScore);
        }

        async private void comeBackHome() {
            await Navigation.PushModalAsync(new MainPage(), false);
        }

        private void verificaZeroLife() {
            if (this.saveMetodo.getScore.life == 0) {
                //bonus di rientro partita salvata e vite esaurite
                this.saveMetodo.getScore.life = 1;
            }
        }
    }
}