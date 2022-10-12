using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MathTrap
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MathPage : ContentPage
    {
        private int index = 10;
        private int level = 0;
        private string operator_;
        private string[] operators = new string[] { "+", "-", ":", "x", "/", "log", "^" };

        public MathPage(int argc)
        {
            InitializeComponent();
            this.label1.Text = "0";
            this.label2.Text = "0";
            this.label3.Text = "0";
            if (argc > 0) {
            
            } else { 
                calculetor(this.index, this.level); 
            }
        }

        private void calculetor(int index, int level) {
            Random r = new Random();
            long operator_int_A;
            long operator_int_B;
            string str;

            if (level % 10 == 2)
            {
                str = operators[r.Next(4, 7)];
                operator_int_A = r.Next(0, 11);
                operator_int_B = r.Next(0, 10);
            }
            else
            {
                str =operators[r.Next(0, 4)];
                operator_int_A = r.Next(index, (index * 10));
                operator_int_B = r.Next(0, index);
            }

            label1.Text = Convert.ToString(operator_int_A);                        
            label2.Text = Convert.ToString(operator_int_B);

            switch (str) {
                 case "+":
                    this.operator_ = Convert.ToString(operator_int_A + operator_int_B);
                    break;

                 case "-":
                    this.operator_ = Convert.ToString(operator_int_A - operator_int_B); 
                    break;

                 case ":":
                    this.operator_ = Convert.ToString(operator_int_A / operator_int_B);
                    break;

                 case "x":
                    this.operator_ = Convert.ToString(operator_int_A * operator_int_B); 
                    break;

                 case "/":
                    this.label1.Text = Convert.ToString((operator_int_A) ^ operator_int_B);
                    this.label2.Text = "1/" + Convert.ToString(operator_int_B);
                    this.operator_ = Convert.ToString(operator_int_A);
                    break;

                 case "^":
                    this.operator_ = Convert.ToString((operator_int_A) ^ operator_int_B);
                    break;

                 case "log":
                    this.label1.Text = Convert.ToString(operator_int_B);
                    this.label2.Text = Convert.ToString((operator_int_A)^operator_int_B);
                    this.operator_ = Convert.ToString(operator_int_A);
                    break;

                 default:
                    this.operator_ = Convert.ToString(operator_int_A ^ 2);
                    break;
                }


        }

        private void onOne(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "1";
            else
                this.label3.Text += "1";
        }

        private void onTwo(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "2";
            else
                this.label3.Text += "2";
        }

        private void onThree(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "3";
            else
                this.label3.Text += "3";
        }

        private void onFour(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "4";
            else
                this.label3.Text += "4";
        }

        private void onFive(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "5";
            else
                this.label3.Text += "5";
        }

        private void onSix(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "6";
            else
                this.label3.Text += "6";
        }

        private void onSeven(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "7";
            else
                this.label3.Text += "7";
        }

        private void onEigth(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "8";
            else
                this.label3.Text += "8";
        }
        private void onNine(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "9";
            else
                this.label3.Text += "9";
        }

        private void onZero(object sender, EventArgs e)
        {
            if (this.label3.Text == "0")
                this.label3.Text = "0";
            else
                this.label3.Text += "0";
        }

        private void onInvio(object sender, EventArgs e)
        {
            if (this.operator_ == this.label3.Text)
                this.label2.Text = "1";
            else
                this.label2.Text += "1";
        }

        private void onCancel(object sender, EventArgs e)
        {
            this.label3.Text = "0";
        }

        async private void onExit(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(), false);
        }
    }
}
