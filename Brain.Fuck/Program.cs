using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Console.WriteLine(Kata.BrainLuck(",+[-.,+]", "Codewars\u00ff"));
        }
    }