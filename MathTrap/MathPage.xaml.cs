﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string operator_;

        private string[] operators;

        private int id_score;
        private long right_counter;
        private long fail_counter;
        private long life;

        private ClassSQL value;


        public MathPage(int index)
        {
            InitializeComponent();

            //assegno la connessione aperta
            value = App.connection;

            this.label1.Text = "0";
            this.label2.Text = "0";
            this.label3.Text = "0";
            this.label4.Text = "";
            this.label5.Text = "";
            this.label6.Text = "";

            /*Carico l'ultima partita aperta
              se: se ritorna la tabella vuota -->primo record di gioco
              altrimenti: carico punteggio salvato nel database
             */
            if ((this.value.item = this.value.GetItemLoad().Result) == null) {
                this.value.item = new TableItem();
                this.value.item.ID = 0;
            } else {
                if (index == 0)
                {
                    //se nuovo record -->chiudo partita vecchia inponendo done = true e aggiorno
                    this.value.item.done = true;
                    this.value.SaveItemAsync(this.value.item);
                    //aggiornando l'indice = 0 composedScore riporta i valori a 0
                    this.value.item.ID = 0;
                }
            }

            this.composedScore(this.value.getItem);

            //aggirno score
            this.label10.Text = Convert.ToString(this.getRight());
            this.label11.Text = Convert.ToString(this.getFail());
            this.label12.Text = Convert.ToString(this.getLife());

            //gioca
            calculetor(this.index, this.level);
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
                if (this.level == 11) {
                    //Superato il livello bonus =10 ricomincio il conteggio
                    this.level = 0;
                    //limite di conversione long to int
                    if (this.index <= Int32.MaxValue) {
                        //raggiunto limite di difficolta randomica
                        this.index += 10;
                    }
                    //aumento punti vita
                    this.setLife(this.getLife() + 1);
                }
                //aumento risposte esatte
                this.score(0);

                calculetor(this.index, this.level);
            }
            else {
                this.label6.Text = "ko";
                this.label3.Text = "0";
                //levo punti vita
                this.setLife(this.getLife() - 1);
                //aumento risposte sbagliate
                this.score(1);
                //controllo vita residua
                if (this.getLife() <= 0) {
                    //fine gioco
                    saveAndExit();
                }
            }

            //aggiorno le etichette

            this.label10.Text = Convert.ToString(this.getRight());
            this.label11.Text = Convert.ToString(this.getFail());
            this.label12.Text = Convert.ToString(this.getLife());
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
            ExitUpDate();
            saveAndExit();
        }

        async private void onSettings(object sender, EventArgs e)
        {
            ExitUpDate();
            await Navigation.PushModalAsync(new SettingPage(this.value), false);
        }

        private void onPoint(object sender, EventArgs e)
        {
            this.tastiera("point");
        }
        
        private void score(int indexe) {

            if (index == 0) {  
                switch (this.label4.Text)   {

                case "+":
                    this.setRight(this.getRight() + 1); 
                    break;
                case "-":
                    this.setRight(this.getRight() + 2); 
                    break;
                case "x":
                    this.setRight(this.getRight() + 3);
                    break;
                case ":":
                    this.setRight(this.getRight() + 4);
                    break;
                case "^":
                    this.setRight(this.getRight() + 5);
                    break;
                case "/":
                    this.setRight(this.getRight() + 6);
                    break;
                default:
                    this.setRight(this.getRight() + 0);
                    break;
            }
            }else{ 
                this.setFail(this.getFail() + 1); 
            }

        }

        async private void saveAndExit() { 
            await Navigation.PushModalAsync(new SavePage(this.value), false);
        }

        // --> SET & GET
        private int getId()
        {
            return this.id_score;
        }

        private void setId(int v)
        {
            this.id_score = v;
        }

        private long getRight()
        {
            return this.right_counter;
        }

        private void setRight(long v)
        {
            this.right_counter = v;
        }

        private long getFail()
        {
            return this.fail_counter;
        }

        private void setFail(long v)
        {
            this.fail_counter = v;
        }

        private long getLife()
        {
            return this.life;
        }

        private void setLife(long v)
        {
            this.life = v;
        }

       
        public void setOperator_(string[] s) {
            this.operators = s;
        }

        //--> FUNZIONI      
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
        private void composedScore(TableItem item)
        {
            int i = 0;
            int j = 0;
            long r = 0;
            long f = 0;
            long l = 5;

            //default le quattro operazioni
            this.operators = this.AggiornaSettings(this.value.getOperandi()); 
           
            if (item.ID > 0)
            {
                i = item.ID;
                r = item.right;
                f = item.fail;
                l = item.life;                
            }
            else {//qualora resume risulta la prima partita
                item.right = r;
                item.fail = f ;
                item.life = l;
                
                //default tutte le operazioni
                item.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
                this.value.item.done = false;
            }
            this.setId(i);
            this.setRight(r);
            this.setFail(f);
            this.setLife(l);
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
                //livello bonus 
                str = operators[r.Next(((operators.Length) - 1), operators.Length)];                         
            }
            else
            { 
                str = operators[r.Next(0, ((operators.Length) - 2))];  
            }

            operator_int_A = r.Next(Convert.ToInt32(index), (Convert.ToInt32(index) * 10));
            operator_int_B = r.Next(1, Convert.ToInt32(index));

            

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
                    operator_int_A = r.Next(1, 11);
                    operator_int_B = r.Next(1, 4);
                    this.label1.Text = Convert.ToString(Math.Pow(operator_int_A, operator_int_B));
                    this.label2.Text = "(1/" + Convert.ToString(operator_int_B)+")";
                    this.label4.Text = "^";
                    this.operator_ = Convert.ToString(operator_int_A);
                    break;

                 case "^":
                    operator_int_A = r.Next(1, 11);
                    operator_int_B = r.Next(1, 4);
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

            this.label1.Text = Convert.ToString(operator_int_A);                        
            this.label2.Text = Convert.ToString(operator_int_B);
            this.label4.Text = str;
            this.label5.Text = "=";

        }

        private void ExitUpDate() {
            this.value.item.ID = this.getId();
            this.value.item.right = this.getRight();
            this.value.item.fail = this.getFail();
            this.value.item.life = this.getLife();
        }

        private string[] AggiornaSettings(string[,] operatori)
        {

            int count = 0;
            string bonus = "x";//-->operatore bonus di default

            for (int i = 0; i < (operatori.Length / this.value.CONST_OPERATOR); i++)
            {
                if (operatori[i, (this.value.CONST_OPERATOR - 2)] == "1")
                {
                    count++;//-->aggiungo un posto
                }
                if (operatori[i, (this.value.CONST_OPERATOR - 1)] == "1") //se bonus -->
                { 
                    if (operatori[i, (this.value.CONST_OPERATOR - 2)] == "1")//se operatore -->
                    {
                        count++;//-->aggiungo un posto
                    }               
                    bonus = operatori[i, 0];
                }
            }

            string[] oper = new string[count];
            count = 0;

            for (int i = 0; i < (operatori.Length / this.value.CONST_OPERATOR); i++)
            {
                if (operatori[i, (this.value.CONST_OPERATOR - 2)] == "1")
                {
                    oper[count] = operatori[i, 0];
                    count++;
                }
            }
            oper[count] = bonus;

            return oper;
        }
    }
}
