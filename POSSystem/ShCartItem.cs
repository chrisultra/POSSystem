using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem
{
    [Serializable]
    public class ShCartItem
    {
        public string num { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string cost { get; set; }
        public string qty { get; set; }
        public string category { get; set; }
    }
}