using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            List<string> mods;
            using (StreamReader file = File.OpenText($@"{getFullpath()}\modids.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                mods = (List<string>)serializer.Deserialize(file, typeof(List<string>));

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

        public List<string> readModIdsfromHTML(string html)
        {
            List<string> mods = new List<string>();
            using (StreamReader file = File.OpenText($@"{getFullpath()}\{html}"))
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(file.ReadToEnd());
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
