using ClassLibrary1.Interface;
using Library;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model
{
    public class Reader : IReader
    {
        public int Reader_id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Issue_tag { get; set; }
        public string Tags_used { get; set; }

        public void ChangePassword()
        {
            
            Console.WriteLine("Введите старый пароль");
            string password=Console.ReadLine();
            using (var db = new LiteDatabase(@"Library"))
            {
                LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                while (true)
                {
                    if (Password == password)
                    {
                        Console.WriteLine("Введите новый пароль");
                        password = Console.ReadLine();
                        Password = password;
                        Console.WriteLine("Пароль успешно изменен");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный пароль");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
    }
}
