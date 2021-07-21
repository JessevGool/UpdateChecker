using System;
using UpdateChecker.SteamMod;


namespace UpdateChecker
{
    class Program
    {

        //STANDARD MOD LINK https://steamcommunity.com/sharedfiles/filedetails/?id={MODID}
        static void Main(string[] args)
        {
            Checker checker = new Checker(false);
            Console.ReadKey();
        }
    }
}
