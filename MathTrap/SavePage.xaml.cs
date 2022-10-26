﻿using System;
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
        private ClassSave saveMetodo;
       

        public SavePage(ClassSave value)
        {
            InitializeComponent(); 
            this.saveMetodo = value;         
        }

        private void onYes(object sender, EventArgs e)
        {
            this.saveMetodo.SaveAsync(this.saveMetodo.getTextSave());
            comeBackHome();
        }

        private void onNo(object sender, EventArgs e)
        {
            comeBackHome();
        }

        async private void comeBackHome() {
            await Navigation.PushModalAsync(new MainPage(), false);
        }

    }
}