using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot.Smev3
{
    public class Smev3Filter
    {
        public bool FilterOnParametrs { get; set; } = false;

        public string version { get; set; }//параметры для второй фильтрации
        public string id { get; set; }
        public string aplication { get; set; }

        public string zone { get; set; }//параметры для первой фильтраии
        public string name { get; set; }
        public bool dispayTestReques { get; set; }
        public bool displayProdRequest { get; set; }

        public static Smev3Filter GetDefault()
        {
            Smev3Filter settings = new Smev3Filter();
            settings.zone = "fed"; //fed || reg
            settings.id = "VS02969v001-RPTN01";
            settings.name = "ИНН";
            settings.aplication = "Межведомственное взаимодействие";
            settings.dispayTestReques = false;
            settings.displayProdRequest = true;

            settings.FilterOnParametrs = true;
            return settings;
        }
    }
}
