using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamBot
{
    public class Games
    {
        public Applist applist { get; set; }
    }

    public class Applist
    {
        public Apps[] apps { get; set; }
    }

    public class Apps
    {
        public string appid { get; set; }
        public string name { get; set; }
    }
}
