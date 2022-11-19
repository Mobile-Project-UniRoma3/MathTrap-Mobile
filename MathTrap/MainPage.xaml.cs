using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private Setting set;

        public MainPage()
        {
            InitializeComponent();
            this.set =  new Setting(App.connection); ;
        }

        async private void onNew(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(0, set), false);
        }

        async private void onResume(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MathPage(1, set),false);
        }

        async private void onHelp(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new HelpPage(),false);
        }

        async private void onScore(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ScorePage(), false);
        }
    }
}