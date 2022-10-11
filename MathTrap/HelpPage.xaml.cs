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
        public HelpPage()
        {
            
            InitializeComponent();
            
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "it": label1.Text = "Benvenuti in MathTrap!!";
                           break;
                case "fr": label1.Text = "Bienvenu en MathTrap!!"; 
                           break;
                case "de": label1.Text = "Willkommen zu MathTrap!!"; 
                           break;
                case "cn": label1.Text = "歡迎來到數學陷阱 ! !"; 
                           break;
                case "es": label1.Text = "¡¡Bienvenidos a MathTrap!!";
                           break;
                default:label1.Text = "Welcome on MathTrap!!"; 
                        break;
            }
        }

        async private void onMain(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(),false);
        }
    }
}