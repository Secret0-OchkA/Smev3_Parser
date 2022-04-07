using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmevBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var worker = new ParserWorker<Smev3Filters>(Smev3Filters.GetDefault());
            var docs = worker.GetDocs();

        }
    }
}

//Вид сведений:fed || reg                                           //zone
//Выбрать: Тестовая среда; Продуктивная среда                       //dispayTestReques || displayProdRequest
//Идентификатор: (пункт не обязателен)                              //ID      ?  который с буковами
//Версия: (пункт не обязателен)                                     //version ?
//Назначение/Наименование:(можно полностью, можно ключевым словом)  //name
//Область применения:                                               //application 
//    (Межведомственное взаимодействие;
//     Межведомственное взаимодействие/Базовый реестр; 
//     Приём заявлений с ЕПГУ;
//     Прием заявлений с ЕПГУ/МФЦ;)   

//Выбрать субъект (не обязательно) Только в smev                    //subject ?  в smev
//Наименование участника взаимодействия/Владелец:                   //Owner   ?  в smev
//    (пункт не обязателен)