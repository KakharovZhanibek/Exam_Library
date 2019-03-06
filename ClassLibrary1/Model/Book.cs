using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model.Model
{
    public enum Status { available,not_available}
    public enum Type { Художественные=1, Документальные=2, Учебные=3, Научные=4, Производственно_технические=5, Программно_методические=6, Справочные=7, Агитационно_пропагандистские=8, Научно_популярные=9, Инструктивные=10 }
    public class Book
    {
        public int S_no { get; set; }
        //public int ISDN { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public Type Type { get; set; }
        public string Author_name { get; set; }
        public DateTime Publish_date { get; set; }
        public int Edition { get; set; }
        public Status Status { get; set; }
    }
}
