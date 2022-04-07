using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot
{
    internal interface IParserFilter
    {
        string zone { get; set; }
        string id { get; set; }
        string version { get; set; }
        string name { get; set; }
        string aplication { get; set; }
    }
}
//zone
//ID      ?  который с буковами
//version ?
//name
//application 

//dispayTestReques || displayProdRequest в svem3

//subject ?  в smev
//Owner   ?  в smev
