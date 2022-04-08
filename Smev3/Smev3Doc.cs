using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot.Smev3
{
    public class Smev3Doc
    {
        public string name { get; set; }
        public string purpose { get; set; }
        public string Id { get; set; }
        public string aplication { get; set; }
        public string version { get; set; }

        public string url { get; set; }

        public static implicit operator Smev3Doc(string[] data)
        {
            Smev3Doc doc = new Smev3Doc();
            doc.name = data[0];
            doc.purpose = data[1];
            doc.Id = data[2];
            doc.aplication = data[3];
            doc.version = data[4];
            doc.url = data[7];
            return doc;
        }
    }
}
