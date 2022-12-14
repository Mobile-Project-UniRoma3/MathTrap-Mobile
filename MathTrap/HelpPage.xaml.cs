using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        private ClassSQL value;
        
        public HelpPage()
        {
            InitializeComponent();

            //assegno la connessione aperta
            this.value = App.connection;
      
            this.label31.Text = "MATHTRAP";
            this.textcell1.Text = this.value.getLang.text;
        }

        async private void onMain(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(),false);
        }
    }
}