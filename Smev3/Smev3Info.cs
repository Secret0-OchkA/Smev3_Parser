using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot.Smev3
{
    public class Smev3Info : IEnumerable
    {
        public string name { get; set; }
        public string version { get; set; }
        List<string> filesId { get; set; } = new List<string>();


        string adress = @"https://smev3.gosuslugi.ru/portal/inquirytype/files/";


        public Smev3Info(string name, string version)
        {
            this.name = name;
            this.version = version;
        }
        public Smev3Info(string name, string version, List<string> urlFiles) : this(name, version)
        {
            for (int i = 0; i < urlFiles.Count; i++)
                filesId.Add(getFileId(urlFiles[i]));
        }

        public string this[int index]
        {
            get { return adress + filesId[index]; }
            set { filesId[index] = getFileId(value); }
        }
        public int Count { get { return filesId.Count; } }

        private string getFileId(string adress)
        {
            return adress.Replace(@"location='/portal/inquirytype/files/", string.Empty)
                         .Replace(@"';event.preventDefault();", string.Empty); 
        }

        public void Add(string urlFile) => filesId.Add(getFileId(urlFile));

        public IEnumerator<string> GetEnumerator()
        {
            return filesId.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)filesId).GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"name:\t{name}\n" +
                                                     $"version:\t{version}\n");
            foreach(string file in filesId)
            {
                result.AppendLine($"idFile:\t{file}");
            }
            return result.ToString();
                
        }
    }
}


