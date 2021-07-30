using Steamworks;
using System;
using UpdateChecker.SteamMod;


namespace UpdateChecker
{
    class Program
    {


        //STANDARD MOD LINK https://steamcommunity.com/sharedfiles/filedetails/?id={MODID}
        static void Main(string[] args)
        {
            SteamAPI.Init();
            Console.Title = "UpdateChecker";
            Console.WriteLine("Do you want to read from a preset?: Y/N");

            string  choice = Console.ReadLine();
            Console.Clear();
            if (choice == "N")
            {
                Checker checker = new Checker(true);
            }
            else
            {
                Checker checker = new Checker(false);
            }
            
            Console.ReadKey();
        }
    }
}
