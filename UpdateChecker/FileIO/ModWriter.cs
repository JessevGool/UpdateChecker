using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            using (StreamReader file = File.OpenText(@"H:\Code\C#\JsonFiles\modids.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                mods = (List<string>)serializer.Deserialize(file, typeof(List<string>));

            }
            return mods;
        }

        public List<ModInfo> readModsfromFile()
        {
            List<ModInfo> mods;
            using (StreamReader file = File.OpenText(@"H:\Code\C#\JsonFiles\mods.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                mods = (List<ModInfo>)serializer.Deserialize(file, typeof(List<ModInfo>));

            }
            return mods;
        }

        public void writeModstoFile(List<ModInfo> mods)
        {
            string json = JsonConvert.SerializeObject(mods.ToArray());
            System.IO.File.WriteAllText(@"H:\Code\C#\JsonFiles\mods.txt", json);
        }

        public void writeModstoFile()
        {
            string json = JsonConvert.SerializeObject(_modIds.ToArray());
            System.IO.File.WriteAllText(@"H:\Code\C#\JsonFiles\modids.txt", json);
        }
    }
}
