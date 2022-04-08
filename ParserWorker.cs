using HtmlAgilityPack;
using SmevBot.Smev3;
using System;
using System.Collections.Generic;

namespace SmevBot
{
    internal class ParserWorker
    {
        private GetResponseSmev3 response { get; set; }
        public ParserWorker(Smev3Filter filter)
        {
            response = new GetResponseSmev3(filter);
        }

        public List<Smev3Info> GetDocs()
        {
            List<Smev3Doc> docs = response.GetUrlCollection();
            List<Smev3Info> filesInfo = response.GetFiles(docs);
            return filesInfo;
        }
    }

    

    

    
}

