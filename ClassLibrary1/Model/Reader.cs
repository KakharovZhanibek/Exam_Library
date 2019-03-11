using Library.LIB.Interface;
using Library;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LIB.Model.Model;

namespace Library.LIB.Model
{
    public class Reader : IUser
    {
        public int Id { get; set ; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Dictionary<Book, DateTime> MyBooks { get; set; } = new Dictionary<Book, DateTime>();

        public void PrintMyBooks()
        {
            if (MyBooks != null)
            {
                foreach (KeyValuePair<Book, DateTime> item in MyBooks)
                {
                    Console.WriteLine("|||");
                    item.Key.PrintInfo();
                    Console.WriteLine("\nДата возврата {0}", item.Value);
                }
            }
            else
            {
                Console.WriteLine("Вы еще не взяли ни одну книгу");
            }
        }
    }
}
