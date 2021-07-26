using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UpdateChecker.SteamMod;

namespace UpdateChecker.FileIO
{
    class ModWriter
    {


        List<string> _modIds { get; set; }
        public ModWriter(List<string> modIds)
        {
            _modIds = modIds;
        }
        public ModWriter()
        {

        }

        public List<string> readModIdsfromFile()
        {
            List<string> mods = new List<string>();
            if (File.Exists($@"{getFullpath()}\modids.json"))
            {
                using (StreamReader file = File.OpenText($@"{getFullpath()}\modids.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    mods = (List<string>)serializer.Deserialize(file, typeof(List<string>));
                }

            }
            else
            {
                using (StreamWriter sw = File.CreateText($@"{getFullpath()}\modids.json"))
                {
                    string _json = JsonConvert.SerializeObject(new List<string> { "PUT", "IN", "THIS", "FORMAT" });
                    sw.Write(_json);
                    sw.Close();
                }
                Console.WriteLine(@$"File: {getFullpath()}\modids.json has been created");
                Console.WriteLine("Put list of ModIds inside using JSON format");
                Console.WriteLine("Application will close in 10 seconds");
                Thread.Sleep(10000);
                System.Environment.Exit(1);
            }
            return mods;
        }

        public List<ModInfo> readModsfromFile()
        {
            try
            {
                List<ModInfo> mods;
                using (StreamReader file = File.OpenText($@"{getFullpath()}\mods.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    mods = (List<ModInfo>)serializer.Deserialize(file, typeof(List<ModInfo>));

                }
                return mods;
            }
            catch
            {
                return new List<ModInfo>();
            }

        }

        public List<string> readModIdsfromHTML()
        {
            List<string> mods = new List<string>();
            var htmlNames = new List<string>();
            if (File.Exists($@"{getFullpath()}\htmlStorage.json"))
            {
                using (StreamReader file = File.OpenText($@"{getFullpath()}\htmlStorage.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    htmlNames = (List<string>)serializer.Deserialize(file, typeof(List<string>));
                    foreach (var htmlName in htmlNames)
                    {
                        using (StreamReader _file = File.OpenText($@"{getFullpath()}\{htmlName}"))
                        {
                            HtmlDocument document = new HtmlDocument();
                            document.LoadHtml(_file.ReadToEnd());
                            var modListClass = document.DocumentNode.SelectSingleNode("//div[@class='mod-list']");
                            var modContainers = modListClass.SelectNodes("//tr[@data-type='ModContainer']");

                            foreach (var container in modContainers)
                            {
                                var id = container.InnerText.Split(new string[] { "http://steamcommunity.com/sharedfiles/filedetails/?id=" }, StringSplitOptions.None)[1];
                                id = id.Replace("\r\n", "");
                                id = id.Replace(" ", "");
                                mods.Add(id);
                            }
                        }
                    }
                  
                }

            }
            else
            {
                using (StreamWriter sw = File.CreateText($@"{getFullpath()}\htmlStorage.json"))
                {
                    List<string> _json = new List<string> { "HTML 1", "HTML 2(optional)" };
                    var _jsonString = JsonConvert.SerializeObject(_json,Formatting.Indented);
                    sw.Write(_jsonString);
                    sw.Close();
                }
                Console.WriteLine(@$"File: {getFullpath()}\htmlStorage.json has been created");
                Console.WriteLine("Put HTML in the same location and input html name inside of the json");
                Console.WriteLine("Application will close in 10 seconds");
                Thread.Sleep(10000);
                System.Environment.Exit(1);
            }
            mods = mods.Distinct().ToList();
            return mods;
        }

        public void writeModstoFile(List<ModInfo> mods)
        {
            string json = JsonConvert.SerializeObject(mods.ToArray(),
                Formatting.Indented);
            System.IO.File.WriteAllText($@"{getFullpath()}\mods.json", json);
        }

        public void writeModIdstoFile()
        {
            string json = JsonConvert.SerializeObject(_modIds.ToArray(),
                Formatting.Indented);
            System.IO.File.WriteAllText($@"{getFullpath()}\modids.json", json);

        }

        private string getFullpath()
        {
            var systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
            var complete = Path.Combine(systemPath, "UpdateChecker");
            if (!Directory.Exists(complete))
            {
                Directory.CreateDirectory(complete);
            }
            return complete;
        }

        private string checkLogDirectory()
        {
            var complete = Path.Combine(getFullpath(), "UpdateLogs");
            if (!Directory.Exists(complete))
            {
                Directory.CreateDirectory(complete);
            }
            return complete;
        }
        public void writetoLogFile(string logText, string fileName)
        {

            System.IO.File.AppendAllText($@"{checkLogDirectory()}\{fileName}.txt", logText);
        }
    }
}
