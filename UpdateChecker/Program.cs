 using System;
using UpdateChecker.SteamMod;


namespace UpdateChecker
{
    class Program
    {

        //STANDARD MOD LINK https://steamcommunity.com/sharedfiles/filedetails/?id={MODID}
        static void Main(string[] args)
        {
            //List<string> modIds = new List<string>();
            //modIds.Add("450814997");
            //modIds.Add("843425103");
            //modIds.Add("843577117");
            //modIds.Add("583496184");
            //modIds.Add("893328083");
            //modIds.Add("1661066023");
            //modIds.Add("497660133");
            //modIds.Add("333310405");
            //modIds.Add("843593391");
            //modIds.Add("583544987");
            //modIds.Add("1779063631");
            //modIds.Add("751965892");
            //modIds.Add("2018593688");
            //modIds.Add("497661914");
            //modIds.Add("1135534951");
            //modIds.Add("825172265");
            //modIds.Add("1673456286");
            //modIds.Add("893339590");
            //modIds.Add("1623498241");
            //modIds.Add("825174634");
            //modIds.Add("2034363662");
            //modIds.Add("1858075458");
            //modIds.Add("1841047025");
            //modIds.Add("2222778153");
            //modIds.Add("2055674861");
            //modIds.Add("2404759841");
            //modIds.Add("549676314");
            //modIds.Add("893349825");
            //modIds.Add("1638341685");
            //modIds.Add("1886782771");
            //modIds.Add("642912021");
            //modIds.Add("1803586009");
            //modIds.Add("1680471603");
            //modIds.Add("2455666943");
            //modIds.Add("1981964169");
            //modIds.Add("520618345");
            //modIds.Add("2348943677");
            //modIds.Add("872287310");
            //modIds.Add("1298165770");
            //modIds.Add("1520786131");
            //modIds.Add("1397683809");
            //modIds.Add("2131539628");
            //modIds.Add("2120367396");
            //modIds.Add("1886784408");
            //modIds.Add("2467590475");
            //modIds.Add("962938144");
            //modIds.Add("718649903");
            //modIds.Add("1368857262");
            //modIds.Add("2214384530");
            //modIds.Add("1329037386");
            //modIds.Add("1252091296");
            //modIds.Add("2185874952");

            //string json = JsonConvert.SerializeObject(modIds.ToArray());
            //System.IO.File.WriteAllText(@"H:\Code\C#\JsonFiles\mods.txt", json);
            //HtmlWeb web = new HtmlWeb();
            //HtmlDocument doc = web.Load("https://steamcommunity.com/sharedfiles/filedetails/?id=450814997");
            //var HeaderNames = doc.DocumentNode.SelectNodes("//div[@class='detailsStatRight']");
            //Console.WriteLine(HeaderNames[2].InnerText);
            Checker checker = new Checker();
            Console.ReadKey();
        }
    }
}
