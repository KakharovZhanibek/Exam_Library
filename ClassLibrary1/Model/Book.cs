using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model.Model
{
    public enum Status { available,not_available}
    public enum TypeBook { Художественные=1, Документальные=2, Учебные=3, Научные=4, Производственно_технические=5, Программно_методические=6, Справочные=7, Агитационно_пропагандистские=8, Научно_популярные=9, Инструктивные=10 }
    public class Book
    {
        public int S_no { get; set; }
        //public int ISDN { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public TypeBook TypeBook { get; set; }
        public string Author_name { get; set; }
        public DateTime Publish_date { get; set; }
        public int Edition { get; set; }
        public Status Status { get; set; }


        public void PrintInfo()
        {
            string str=string.Format ("Номер книги: {0}\nНазвание: {1}\nКод: {2}\nТип: {3}\nИмя автора: {4}\nДата публикации: {5}\nСтатус: {6}\n",
                S_no,Name,Code,TypeBook,Author_name,Publish_date,Edition,Status);
            Console.WriteLine("___________________________\n");
            Console.WriteLine(str);
            Console.WriteLine("___________________________\n");
        }
    }
}
