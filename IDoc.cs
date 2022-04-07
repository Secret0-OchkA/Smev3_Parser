using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot
{
    internal interface IDoc
    {
        string name { get; set; }
        string purpose { get; set; }
        string Id { get; set; }
        string aplication { get; set; }
        string version { get; set; }

        string url { get; set; }
    }
}
//zone
//dispayTestReques || displayProdRequest
//ID      ?  который с буковами
//version ?
//name
//application 


