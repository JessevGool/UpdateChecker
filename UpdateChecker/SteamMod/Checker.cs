using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UpdateChecker.FileIO;
using UpdateChecker.Scraper;

namespace UpdateChecker.SteamMod
{
    public class Checker
    {
        List<ModInfo> _mods { get; set; }
        List<string> _modIds { get; set; }
        List<ModInfo> _lastInfo { get; set; }

        SteamUpdateScraper scraper = new SteamUpdateScraper();

        ModWriter modWriter = new ModWriter();
        public Checker()
        {
            ModWriter writer = new ModWriter();
            _modIds = writer.readModIdsfromFile();
            scraper._modIds = _modIds;
            _lastInfo = scraper.gatherModInfo();
            checkForUpdates();
        }
        public Checker(List<string> modIds)
        {
            _modIds = modIds;
            scraper._modIds = _modIds;
            _lastInfo = modWriter.readModsfromFile();
            checkForUpdates();
        }

        private void checkForUpdates()
        {
            _mods = scraper.gatherModInfo();
            sortLists(_mods, _lastInfo);
            if(_mods.Count == _lastInfo.Count)
            {
                for (int i = 0; i < _mods.Count; i++)
                {
                    if (_mods[i]._modName == _lastInfo[i]._modName && _mods[i]._modId == _lastInfo[i]._modId && _mods[i]._lastUpdateTime == _lastInfo[i]._lastUpdateTime)
                    {
                    }
                    else
                    {
                        Console.WriteLine("---------");
                        Console.WriteLine($"-------{_mods[i]._modName} HAS BEEN UPDATED-------");
                        Console.WriteLine($"-------{DateTime.Now.ToString()}-------");
                        Console.WriteLine("---------");
                    }

                }
            }
            else
            {
                //Incase json isn't filled correctly
                _lastInfo = _mods;
                for (int i = 0; i < _mods.Count; i++)
                {
                    if (_mods[i]._modName == _lastInfo[i]._modName && _mods[i]._modId == _lastInfo[i]._modId && _mods[i]._lastUpdateTime == _lastInfo[i]._lastUpdateTime)
                    {
                    }
                    else
                    {
                        Console.WriteLine("---------");
                        Console.WriteLine($"-------{_mods[i]._modName} HAS BEEN UPDATED-------");
                        Console.WriteLine($"-------{DateTime.Now.ToString()}-------");
                        Console.WriteLine("---------");
                    }

                }
            }
            
            _lastInfo = _mods;
            modWriter.writeModstoFile(_mods);
            Console.WriteLine($"Last update: {DateTime.Now}");
            Thread.Sleep(3600 * 1000);
            checkForUpdates();
        }

        private void sortLists(List<ModInfo> mods, List<ModInfo> lastInfo)
        {
            _mods = mods.OrderBy(mod => mod._modName).ToList();
            _lastInfo = lastInfo.OrderBy(mod => mod._modName).ToList();
        }
    }
}


