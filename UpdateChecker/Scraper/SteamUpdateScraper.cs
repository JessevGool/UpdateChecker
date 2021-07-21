using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpdateChecker.FileIO;
using UpdateChecker.SteamMod;

namespace UpdateChecker.Scraper
{
    class SteamUpdateScraper
    {
        private const string workshopLink = "https://steamcommunity.com/sharedfiles/filedetails/?id=";
        private List<ModInfo> _mods { get; set; }
        public List<string> _modIds { get; set; }

        private List<Task> tasks = new List<Task>();

        private HtmlWeb web = new HtmlWeb();

        public SteamUpdateScraper()
        {

        }
        public SteamUpdateScraper(List<ModInfo> mods)
        {
            _mods = mods;
            setModIds();
        }

        public SteamUpdateScraper(List<string> modIds)
        {
            _modIds = modIds;
        }

        public List<ModInfo> gatherModInfo()
        {
            List<ModInfo> mods = new List<ModInfo>();
            if (_modIds != null)
            {
                foreach (var mod in _modIds)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        mods.Add(gatherInfo(mod));
                    }));
                }
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    t.Wait();
                }
                catch { }
                if (t.Status == TaskStatus.RanToCompletion)
                {

                    return mods;

                }
            }
            else
            {
                ModWriter modWriter = new ModWriter();
                _modIds = modWriter.readModIdsfromFile();
                gatherModInfo();
            }
            return mods;
        }

        private void setModIds()
        {
            foreach (var mod in _mods)
            {
                _modIds.Add(mod._modId);
            }
        }

        private ModInfo gatherInfo(string modId)
        {
            //TODO IMPLEMENT SMART WAY TO FIX THIS, ASNYC AWAIT
            //Probable error cause is code continueing before page is loaded
            HtmlDocument doc = web.Load($"{workshopLink}{modId}");
            var headerNames = doc.DocumentNode.SelectNodes("//div[@class='detailsStatRight']");
            var titleName = doc.DocumentNode.SelectSingleNode("//div[@class='workshopItemTitle']");
            if (headerNames != null && titleName != null && headerNames.Count > 2)
            {

                ModInfo modInfo = new ModInfo(titleName.InnerText, modId, headerNames[2].InnerText);
                return modInfo;
            }
            else if (headerNames != null && titleName != null)
            {
                ModInfo modInfo = new ModInfo(titleName.InnerText, modId, "Has never been updated");
                return modInfo;
            }
            else
            {
                ModInfo modInfo = new ModInfo("ERROR OCCURED", modId, "ERROR OCCURED");
                Console.WriteLine($"Loading {modId} failed, please check if this error keeps occuring");
                return modInfo;
            }

        }

    }
}
