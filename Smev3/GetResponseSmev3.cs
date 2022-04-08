using HtmlAgilityPack;
using SmevBot.Smev3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot
{
    public class GetResponseSmev3
    {
        string adress = @"https://smev3.gosuslugi.ru/portal/inquirytype?";

        private Smev3Filter filter { get; set; }

        public GetResponseSmev3(Smev3Filter filter)
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
                bool version = filter.version == null ? true : doc.version.Equals(filter.version);
                bool id = filter.id == string.Empty ? true : doc.Id.Equals(filter.id);
                bool apclication = doc.aplication.Equals(filter.aplication);

                return version && id && apclication;
            }

            return true;
        }

        public List<Smev3Doc> GetUrlCollection()
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
                    if (i == 0)//получение url документа
                    {
                        string[] args = td.ChildNodes[0].Attributes[0].Value.Split('?');
                        data[7] = args.Length > 1 ? args[1] : null;
                    }

                    if (i == 6)//получение всех остальных данных из таблицы
                    {
                        Smev3Doc doc = data;

                        if (SecondFilter(doc))
                        {
                            PrintData(doc, curPage);
                            docs.Add(doc);
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

        public List<Smev3Info> GetFiles(List<Smev3Doc> docs)
        {
            HtmlWeb web = new HtmlWeb();
            List<Smev3Info> filesInfo = new List<Smev3Info>();

            foreach (Smev3Doc doc in docs)
            {
                HtmlDocument htmlDoc = web.Load(adress + "type=getOneMS&" + doc.url);

                Smev3Info info = new Smev3Info(doc.name, doc.version);
                foreach (var node in htmlDoc.DocumentNode.SelectNodes("//tr/td/button[@class='btn btn-info']/@onclick"))
                {
                    string str = (from attr in node.Attributes
                                 where attr.Name == "onclick"
                                 select attr.Value).First();

                    info.Add(str);
                }

                filesInfo.Add(info);
            }

            return filesInfo;
        }
    }
}
