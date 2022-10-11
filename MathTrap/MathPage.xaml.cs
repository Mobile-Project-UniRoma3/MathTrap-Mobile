using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MathPage : ContentPage
    {
        private int indice = 0;
        private double divisione = 0;
        private long operazione = 0;

        public MathPage(int argc)
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
            label3.Text = "0";
            if (argc > 0) { } else { }
        }

        async private void onOne(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "1";
            else
                label3.Text += "1";
        }

        async private void onTwo(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "2";
            else
                label3.Text += "2";
        }

        async private void onThree(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "3";
            else
                label3.Text += "3";
        }

        async private void onFour(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "4";
            else
                label3.Text += "4";
        }

        async private void onFive(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "5";
            else
                label3.Text += "5";
        }

        async private void onSix(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "6";
            else
                label3.Text += "6";
        }

        async private void onSeven(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "7";
            else
                label3.Text += "7";
        }

        async private void onEigth(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "8";
            else
                label3.Text += "8";
        }
        async private void onNine(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "9";
            else
                label3.Text += "9";
        }

        async private void onZero(object sender, EventArgs e)
        {
            if (label3.Text == "0")
                label3.Text = "0";
            else
                label3.Text += "0";
        }

        async private void onInvio(object sender, EventArgs e)
        {
            if (label2.Text == label3.Text)
                label2.Text = "1";
            else
                label2.Text += "1";
        }

        async private void onCancel(object sender, EventArgs e)
        {
            label3.Text = "0";
        }

        async private void onExit(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(), false);
        }
    }
}
