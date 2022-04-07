using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace SmevBot
{
    internal class ParserWorker
    {
        private GetResponseSmev3 response { get; set; }
        public ParserWorker(Smev3Filters filter)
        {
            response = new GetResponseSmev3(filter);
        }

        public List<Smev3Doc> GetDocs()
        {
            return response.GetCollection();
        }
    }

    public class GetResponseSmev3
    {
        string adress = @"https://smev3.gosuslugi.ru/portal/inquirytype?";
        private Smev3Filters filter { get; set; }

        public GetResponseSmev3(Smev3Filters filter)
        {
            this.filter = filter;
        }

        string MakeParametrs(int page)
        {
            return $"type=getList" +
                   $"&sortBy=created" +
                   $"&sortDirection=desc" +
                   $"&title={filter.name}" +
                   $"&dateFrom=" +
                   $"&dateTo=" +
                   $"&page={page}" +
                   $"&zone={filter.zone}" +
                   $"&displayTestRequests={filter.dispayTestReques}" +
                   $"&displayProdRequests={filter.displayProdRequest}";
        }

        private void PrintData(Smev3Doc doc, int curPage)
        {
            Console.WriteLine($"=====================CURRENT-PAGE {curPage}=====================");
            Console.WriteLine($"name:\t{doc.name}\n" +
                              $"purpose:\t{doc.purpose}\n" +
                               $"Id:\t{doc.Id}\n" +
                               $"aplicatio:\t{doc.aplication}\n" +
                               $"version:\t{doc.version}\n" +
                               $"url:\t{doc.url}\n\n\n");

        }

        private bool SecondFilter(Smev3Doc doc)
        {
            if (filter.FilterOnParametrs)
            {
                bool version = filter.version == string.Empty ? true : doc.version.Equals(filter.version);
                bool id = filter.id == string.Empty ? true : doc.Id.Equals(filter.id);
                bool apclication = doc.aplication.Equals(filter.aplication);

                return version && id && apclication;
            }

            return true;
        }

        public List<Smev3Doc> GetCollection()
        {
            HtmlWeb web = new HtmlWeb();
            List<Smev3Doc> docs = new List<Smev3Doc>();
            
            int curPage = 1;
            do
            {
                HtmlDocument htmlDoc = web.Load(adress + MakeParametrs(curPage));
                string[] data = new string[8];
                int i = 0;
                foreach (var td in htmlDoc.DocumentNode.SelectNodes("//tr/td"))
                {
                    data[i] = td.InnerText;
                    if(i == 0)
                    {
                        data[7] = td.ChildNodes[0].Attributes[0].Value;
                    }

                    if (i == 6)
                    {
                        Smev3Doc doc = data;

                        if (SecondFilter(doc))
                        {
                            PrintData(doc, curPage);
                            //docs.Add(doc);
                        }

                        data = new string[8];

                        i = 0;
                        continue;
                    }
                    i++;
                }

                if (htmlDoc.DocumentNode.SelectNodes("//tr[@id]") == null) break;

                curPage++;
            } while (true);

            return docs;
        }
    }

    public class Smev3Filters : IParserFilter
    {
        public bool FilterOnParametrs { get; set; } = false;

        public string version { get; set; }//параметры для второй фильтрации
        public string id { get; set; }
        public string aplication { get; set; }

        public string zone { get; set; }//параметры для первой фильтраии
        public string name { get; set; }
        public bool dispayTestReques { get; set; }
        public bool displayProdRequest { get; set; }

        public static Smev3Filters GetDefault()
        {
            Smev3Filters settings = new Smev3Filters();
            settings.zone = "reg"; //fed || reg
            settings.id = string.Empty;
            settings.aplication = string.Empty;
            settings.name = string.Empty;
            settings.aplication = string.Empty;
            settings.dispayTestReques = false;
            settings.displayProdRequest = true;
            return settings;
        }
    }

    public class Smev3Doc : IDoc
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

