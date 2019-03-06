using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model.Model
{
    public class Feedback
    {
        public int Issue_no { get; set; }
        public int S_No { get; set; }
        public string ISDN { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int Type { get; set; }
        public string Borrow_name{ get; set; }
        public string Author { get; set; }
        public DateTime Issue_date { get; set; }
        public DateTime Return_date { get; set; }
    }
}
