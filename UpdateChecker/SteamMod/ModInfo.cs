using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateChecker.SteamMod
{
    class ModInfo
    {
        public string _modName { get; set; }
        public string _modId { get; set; }
        public string _lastUpdateTime { get; set; }

        public ModInfo(string modName, string modId, string lastUpdateTime)
        {
            _modName = modName;
            _modId = modId;
            _lastUpdateTime = lastUpdateTime;
        }
    }
}
