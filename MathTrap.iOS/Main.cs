﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MathTrap.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        [Obsolete]
        static void Main(string[] args)
        {
            try {
                // if you want to use a different Application Delegate class from "AppDelegate"
                // you can specify it here.
                UIApplication.Main(args, null, typeof(AppDelegate)); 
            } catch (Exception e) {   }
        }

        //public class MathTrap : UIApplication
        //{
        //...etc...
        //}

        //public class AppDelegate : UIApplicationDelegate
        //{
        //..etc...
        //}
    }
}
