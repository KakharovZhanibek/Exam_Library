using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model.Model
{
    public class ServiceAdministrator
    {
        public static int count = 1;
        public static bool RegNewReader()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    Reader reader = new Reader();
                    reader.Reader_id = count;
                    count++;

                    Console.WriteLine("Введите имя");
                    reader.Name = Console.ReadLine();

                    Console.WriteLine("Введите логин");
                    while (true)
                    {
                        reader.Login = Console.ReadLine();
                        if (readers.FindOne(f => f.Login == reader.Login)==null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Такой логин уже существует");
                        }
                    }

                    Console.WriteLine("Введите пароль");
                    reader.Password = Console.ReadLine();
                    
                    reader.IsBlock = false;

                    Console.WriteLine("Введите адрес");
                    reader.Address = Console.ReadLine();

                    Console.WriteLine("Введите контакты");
                    reader.Contact = Console.ReadLine();

                    Console.WriteLine("Введите email");
                    reader.Email = Console.ReadLine();

                    Console.WriteLine("Введите Issue_tag");
                    reader.Issue_tag = Console.ReadLine();

                    Console.WriteLine("Введите Issue_tag");
                    reader.Tags_used = Console.ReadLine();

                    readers.Insert(reader);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
        }

        public static void RedactReader()
        {
            Console.WriteLine("Введите Login пользователя в которого хотите внести изменения");
            string login;
            login = Console.ReadLine();
            Console.Clear();
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");

                    Reader reader = readers.FindOne(f => f.Login == login);
                    Console.WriteLine("Что вы хотите изменить?");


                    while (true)
                    {
                        Console.WriteLine("1: Логин\n2: Пароль\n3: Адрес\n4: Контакты\n5: Email");
                        if (Int32.Parse(Console.ReadLine()) == 1)
                        {
                            Console.WriteLine("Введите новый логин");
                            string new_login;
                            new_login = Console.ReadLine();
                            while (true)
                            {
                                if (readers.FindOne(f => f.Login == new_login) == null)
                                {
                                    reader.Login = new_login;
                                    readers.Update(reader);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Такой логин уже существует");
                                }
                            }
                            break;
                        }
                        else if (Int32.Parse(Console.ReadLine()) == 2)
                        {
                            Console.WriteLine("Введите новый пароль");
                            reader.Password = Console.ReadLine();
                            readers.Update(reader);
                            Console.WriteLine("Пароль успешно изменен");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else if (Int32.Parse(Console.ReadLine()) == 3)
                        {
                            Console.WriteLine("Введите новый адрес");
                            reader.Address = Console.ReadLine();
                            readers.Update(reader);
                            Console.WriteLine("Адрес успешно изменен");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else if (Int32.Parse(Console.ReadLine()) == 4)
                        {
                            Console.WriteLine("Введите новые контакты");
                            reader.Password = Console.ReadLine();
                            readers.Update(reader);
                            Console.WriteLine("Контакты успешно изменены");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else if (Int32.Parse(Console.ReadLine()) == 5)
                        {
                            Console.WriteLine("Введите новый email");
                            reader.Password = Console.ReadLine();
                            readers.Update(reader);
                            Console.WriteLine("Email успешно изменен");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Выберите один из пунктов");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool ReaderIsExist(string login)
        {
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                Reader reader = readers.FindOne(u => u.Login == login);
                if (reader != null)
                    return true;
                else return false;
            }
        }


        public static void GetAllReaders()
        {
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                foreach (var reader in readers.FindAll())
                {
                    Console.WriteLine("______________________________\n");
                    Console.WriteLine("ID: {0}\nИмя :{1}\nЛогин :{2}\nПароль :{3}\nСтатус :{4}\nАдрес :{5}\nКонтакты :{6}\nEmail :{7}",
                        reader.Reader_id,
                        reader.Name,
                        reader.Login,
                        reader.Password,
                        reader.Address,
                        reader.Contact,
                        reader.Email);
                    Console.WriteLine("\n______________________________");
                }
            }
        }


        public static void AddNewBook()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");

                    Book book = new Book();

                    Console.WriteLine("Введите номер книги\n");
                    book.S_no = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите имя книги\n");
                    book.Name = Console.ReadLine();

                    Console.WriteLine("Введите код книги\n");
                    book.Code = Convert.ToInt32(Console.ReadLine());
                    while (true)
                    {
                        Console.WriteLine("Выберите тип книги\n");

                        Console.WriteLine("1: {0}\n2: {0}\n3: {0}\n4: {0}\n5: {0}\n6: {0}\n7: {0}\n8: {0}\n9: {0}\n10: {0}\n",
                            TypeBook.Художественные,
                            TypeBook.Документальные,
                            TypeBook.Учебные,
                            TypeBook.Научные,
                            TypeBook.Производственно_технические,
                            TypeBook.Программно_методические,
                            TypeBook.Справочные,
                            TypeBook.Агитационно_пропагандистские,
                            TypeBook.Научно_популярные,
                            TypeBook.Инструктивные);

                        book.TypeBook = (TypeBook)Convert.ToInt32(Console.ReadLine());
                        if ((int)book.TypeBook < 1 || (int)book.TypeBook > 10)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Выберите один из пунктов");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    Console.WriteLine("Введите имя автора");
                    book.Author_name = Console.ReadLine();

                    Console.WriteLine("Введите дату публикации");
                    book.Publish_date = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Введите издание книги");
                    book.Edition = Convert.ToInt32(Console.ReadLine());

                    book.Status = Status.available;

                    books.Insert(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void FindBook()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");
                    Console.WriteLine("Выберите по какому свойству хотите искать книгу");
                    while (true)
                    {
                        Console.WriteLine("1: Номер\n2: Название\n3: Код\n4: Тип\n5: Имя автора\n6: Дата публикации\n");
                        int x = 0;
                        x = Convert.ToInt32(Console.ReadLine());
                        if (x == 1)
                        {
                            Console.WriteLine("Введите номер книги");
                            x = Convert.ToInt32(Console.ReadLine());
                            books.FindOne(f => f.S_no == x).PrintInfo();
                        }
                        if (x == 2)
                        {
                            string name;
                            Console.WriteLine("Введите название книги");
                            name = Console.ReadLine();
                            books.FindOne(f => f.Name == name).PrintInfo();
                        }
                        if (x == 3)
                        {
                            Console.WriteLine("Введите код книги");
                            x = Convert.ToInt32(Console.ReadLine());
                            books.FindOne(f => f.Code == x).PrintInfo();
                        }
                        if (x == 4)
                        {
                            while (true)
                            {
                                Console.WriteLine("Выберите тип книги\n");

                                Console.WriteLine("1: {0}\n2: {0}\n3: {0}\n4: {0}\n5: {0}\n6: {0}\n7: {0}\n8: {0}\n9: {0}\n10: {0}\n",
                                    TypeBook.Художественные,
                                    TypeBook.Документальные,
                                    TypeBook.Учебные,
                                    TypeBook.Научные,
                                    TypeBook.Производственно_технические,
                                    TypeBook.Программно_методические,
                                    TypeBook.Справочные,
                                    TypeBook.Агитационно_пропагандистские,
                                    TypeBook.Научно_популярные,
                                    TypeBook.Инструктивные);

                                x = Convert.ToInt32(Console.ReadLine());

                                // book.TypeBook = (TypeBook)Convert.ToInt32(Console.ReadLine());
                                if (x > 0 && x < 11)
                                {
                                    foreach (Book book in books.FindAll().Where(w => w.TypeBook == (TypeBook)x))
                                    {
                                        book.PrintInfo();
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Выберите один из пунктов");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                        }
                        if (x == 5)
                        {
                            Console.WriteLine("Введите дату публикации книги\n\tdd:MM:yyyy");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            foreach (Book book in books.FindAll().Where(w => w.Publish_date == date))
                            {
                                book.PrintInfo();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void GetAllBlockedReaders()
        {
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                foreach (var reader in readers.FindAll().Where(w => w.IsBlock = true))
                {
                    Console.WriteLine("______________________________\n");
                    Console.WriteLine("ID: {0}\nИмя :{1}\nЛогин :{2}\nПароль :{3}\nАдрес :{4}\nКонтакты :{5}\nEmail :{6}",
                        reader.Reader_id,
                        reader.Name,
                        reader.Login,
                        reader.Password,
                        reader.Address,
                        reader.Contact,
                        reader.Email);
                    Console.WriteLine("\n______________________________");
                }
            }
        }
        public static void BlockUserById(int reader_id)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");

                    Reader reader = readers.FindOne(f => f.Reader_id == reader_id);
                    reader.IsBlock = true;

                    readers.Update(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void BlockUserByLogin(string login)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");

                    Reader reader = readers.FindOne(f => f.Login == login);
                    reader.IsBlock = true;

                    readers.Update(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
