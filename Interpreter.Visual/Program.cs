﻿using System.Diagnostics;
using static System.Console;
namespace InterpreterDyZ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        do
        {   
            Game myGame= new Game();
            myGame.Start();
        
            Principal.Main();
            
            ResetColor();

        }while(true);    

        
        }
    }
}