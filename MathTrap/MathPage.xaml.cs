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
        //dati di partenza
        private long index = 10;
        private long level = 1;
        private string operator_ ; 
        private string[] operators = new string[] { "+", "-", ":", "x", "/", "^" };
        
        private ClassSQL value;

        public MathPage(int index)
        {
            InitializeComponent();
            value = new ClassSQL();

            this.label1.Text = "0";
            this.label2.Text = "0";
            this.label3.Text = "0";
            this.label4.Text = "";
            this.label5.Text = "";
            this.label6.Text = "";

            TableItem item = new TableItem();
            if (index > 0) {
                //carico punteggio salvato             
                item = this.value.GetItemLoad().Result;                                        
            } 
            this.value.getSave().composedScore(item);

            //aggirno score
            this.label10.Text = Convert.ToString(this.value.getSave().getRight());
            this.label11.Text = Convert.ToString(this.value.getSave().getFail());
            this.label12.Text = Convert.ToString(this.value.getSave().getLife());

            //gioca
            calculetor(this.index, this.level); 
        }

        private void calculetor(long index, long level) {
            Random r = new Random();
            long operator_int_A;
            long operator_int_B;
            string str;

            this.label6.Text = "";
            this.label3.Text = "0";

            if (level % 10 == 0)
            {
                //livello bonus --> potenza o radice esponenti compresi tra 1 e 3
                str = operators[r.Next(4, 6)];
                operator_int_A = r.Next(1, 11);
                operator_int_B = r.Next(1, 4);                
            }
            else
            {
                str =operators[r.Next(0, 4)];
                operator_int_A = r.Next(Convert.ToInt32(index), (Convert.ToInt32(index) * 10));
                operator_int_B = r.Next(1, Convert.ToInt32(index));
            }

            this.label1.Text = Convert.ToString(operator_int_A);                        
            this.label2.Text = Convert.ToString(operator_int_B);
            this.label4.Text = str;
            this.label5.Text = "=";

            switch (str) {
                 case "+":
                    this.operator_ = Convert.ToString(operator_int_A + operator_int_B);
                    break;

                 case "-":
                    this.operator_ = Convert.ToString(operator_int_A - operator_int_B); 
                    break;

                 case ":":
                    double a, b;
                    a = operator_int_A;
                    b = operator_int_B;
                    this.operator_ = Convert.ToString(Math.Round((a / b), 2));
                    break;

                 case "x":
                    this.operator_ = Convert.ToString(operator_int_A * operator_int_B); 
                    break;

                 case "/":
                    this.label1.Text = Convert.ToString(Math.Pow(operator_int_A, operator_int_B));
                    this.label2.Text = "(1/" + Convert.ToString(operator_int_B)+")";
                    this.label4.Text = "^";
                    this.operator_ = Convert.ToString(operator_int_A);
                    break;

                 case "^":
                    this.label2.Text = "(" + Convert.ToString(operator_int_B) + ")";
                    this.operator_ = Convert.ToString(Math.Pow(operator_int_A, operator_int_B));
                    break;

                /* case "log":
                    this.label1.Text = Convert.ToString(operator_int_B);
                    this.label2.Text = Convert.ToString(Math.Pow(operator_int_A, operator_int_B));
                    this.operator_ = Convert.ToString(operator_int_A);
                    break;*/

                default:
                    this.operator_ = Convert.ToString(Math.Pow(operator_int_A, 2));
                    break;
                }


        }

        private void onOne(object sender, EventArgs e)
        {
            this.tastiera("1");
        }

        private void onTwo(object sender, EventArgs e)
        {
            this.tastiera("2");
        }

        private void onThree(object sender, EventArgs e)
        {
            this.tastiera("3");
        }

        private void onFour(object sender, EventArgs e)
        {
            this.tastiera("4");
        }

        private void onFive(object sender, EventArgs e)
        {
            this.tastiera("5");
        }

        private void onSix(object sender, EventArgs e)
        {
            this.tastiera("6");
        }

        private void onSeven(object sender, EventArgs e)
        {
            this.tastiera("7");
        }

        private void onEigth(object sender, EventArgs e)
        {
            this.tastiera("8");
        }
        private void onNine(object sender, EventArgs e)
        {
            this.tastiera("9");
        }

        private void onZero(object sender, EventArgs e)
        {
            this.tastiera("0");
        }

        private void onInvio(object sender, EventArgs e)
        {

            if (this.label3.Text.Equals(this.operator_)) { 
                this.label6.Text = "ok";
                this.level += 1;
                if (this.level==11) { 
                    //Superato il livello bonus =10 ricomincio il conteggio
                    this.level = 0;
                    //limite di conversione long to int
                    if (this.index <= Int32.MaxValue) {
                        //raggiunto limite di difficolta randomica
                        this.index += 10; 
                    }
                    //aumento punti vita
                    this.value.getSave().setLife(this.value.getSave().getLife() + 1);                 
                }
                //aumento risposte esatte
                this.value.getSave().setRight(this.value.getSave().getRight() + 1);
           
                calculetor(this.index, this.level);
            }            
            else {
                this.label6.Text = "ko";
                this.label3.Text = "0";
                //levo punti vita
                this.value.getSave().setLife(this.value.getSave().getLife() - 1);
                //aumento risposte sbagliate
                this.value.getSave().setFail(this.value.getSave().getFail() + 1);
                //controllo vita residua
                if (this.value.getSave().getLife() <= 0) {
                    //fine gioco
                    saveAndExit();
                }
            }

            //aggiorno le etichette
            this.label10.Text = Convert.ToString(this.value.getSave().getRight());
            this.label11.Text = Convert.ToString(this.value.getSave().getFail());
            this.label12.Text = Convert.ToString(this.value.getSave().getLife());    
        }

        private void tastiera(string numero) {

            switch (numero) {
                case "cancel": this.label3.Text = "0";
                               break;
                case "point": if(!this.label3.Text.Contains("."))
                                 this.label3.Text += ".";
                              break;
                default: if (this.label3.Text == "0")
                         {
                            this.label3.Text = numero;
                         }
                         else
                         {
                            if (this.label3.Text.Contains(".")) 
                            {
                                if (((this.label3.Text.Length)-(this.label3.Text.IndexOf("."))) < 3) 
                                {
                                    this.label3.Text += numero;
                                } 
                            } 
                            else 
                            {
                                this.label3.Text += numero; 
                            }                           
                         }
                         break;
            }          
        }

        private void onCancel(object sender, EventArgs e)
        {
            this.tastiera("cancel");
        }

        private void onSkip(object sender, EventArgs e)
        {
            calculetor(this.index, this.level);
        }

        private void onExit(object sender, EventArgs e)
        {
            saveAndExit();
        }

        async private void saveAndExit() { 
            await Navigation.PushModalAsync(new SavePage(this.value), false);
        }

        private void onPoint(object sender, EventArgs e)
        {
            this.tastiera("point");
        }

    }
}
