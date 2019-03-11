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
        public enum AdminStatusOFAutorization { status01, status02, status03 }

        public static List<string> ReadersAndBooksRecords = new List<string>();

        public static List<Feedback> feedbacks = new List<Feedback>();


        public static bool Registration(Administrator admin)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Administrator> admins = db.GetCollection<Administrator>("Administrator");
                    admins.Insert(admin);
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

        public static bool UserIsExist(string login)
        {
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Administrator> admins = db.GetCollection<Administrator>("Administrator");
                Administrator admin = admins.FindOne(u => u.Login == login);
                if (admin != null)
                    return true;
                else return false;
            }
        }
        public static bool FirstAdminIsExist()
        {
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Administrator> admins = db.GetCollection<Administrator>("Administrator");

                if (admins != null)
                    return true;
                else return false;
            }
        }

        public static void ChangePassword(Administrator admin)
        {

            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Administrator> admins = db.GetCollection<Administrator>("Administrator");
                    foreach (Administrator item in admins.FindAll())
                    {
                        Console.WriteLine(item.Name);
                        Console.WriteLine(item.Login);
                        Console.WriteLine(item.Password);
                    }
                    while (true)
                    {
                        Console.WriteLine("Введите старый пароль");
                        string password = Console.ReadLine();
                        if (admin.Password == password)
                        {
                            Console.WriteLine("Введите новый пароль");
                            password = Console.ReadLine();
                            admin.Password = password;
                            admins.Update(admin);
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }


        public static bool RegNewReader()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    Reader reader = new Reader();

                    Console.WriteLine("Введите имя");
                    reader.Name = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("Введите логин");

                        reader.Login = Console.ReadLine();

                        if (readers.FindOne(f => f.Login == reader.Login) == null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Такой логин уже существует");
                            Console.ReadKey();
                            Console.Clear();
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


                    Console.WriteLine("1: Логин\n2: Пароль\n3: Адрес\n4: Контакты\n5: Email");

                    switch (GetPunctMenu())
                    {
                        case 1:
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
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("Введите новый пароль");
                                reader.Password = Console.ReadLine();
                                readers.Update(reader);
                                Console.WriteLine("Пароль успешно изменен");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        case 3:
                            {
                                Console.WriteLine("Введите новый адрес");
                                reader.Address = Console.ReadLine();
                                readers.Update(reader);
                                Console.WriteLine("Адрес успешно изменен");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        case 4:
                            {
                                Console.WriteLine("Введите новые контакты");
                                reader.Contact = Console.ReadLine();
                                readers.Update(reader);
                                Console.WriteLine("Контакты успешно изменены");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        case 5:
                            {
                                Console.WriteLine("Введите новый email");
                                reader.Email = Console.ReadLine();
                                readers.Update(reader);
                                Console.WriteLine("Email успешно изменен");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Выберите один из пунктов");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
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
                foreach (Reader reader in readers.FindAll())
                {
                    Console.WriteLine("______________________________\n");
                    Console.WriteLine("ID: {0}\nИмя :{1}\nЛогин :{2}\nПароль :{3}\nЗаблокирован :{4}\nАдрес :{5}\nКонтакты :{6}\nEmail :{7}",
                        reader.Id,
                        reader.Name,
                        reader.Login,
                        reader.Password,
                        reader.IsBlock,
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
                        int x = 0;
                        Console.WriteLine("Выберите тип книги\n");

                        Console.WriteLine("1: {0}\n2: {1}\n3: {2}\n4: {3}\n5: {4}\n6: {5}\n7: {6}\n8: {7}\n9: {8}\n10: {9}\n",
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

                        if (x >= 1 && x <= 10)
                        {
                            book.TypeBook = (TypeBook)x;

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

                    Console.WriteLine("1: Номер\n2: Название\n3: Код\n4: Тип\n5: Имя автора\n6: Дата публикации\n");
                    int x;
                    switch (GetPunctMenu())
                    {
                        case 1:
                            {
                                Console.WriteLine("Введите номер книги");
                                x = Convert.ToInt32(Console.ReadLine());
                                if (books.FindOne(f => f.S_no == x) != null)
                                {
                                    books.FindOne(f => f.S_no == x).PrintInfo();
                                }
                                else
                                {
                                    Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;
                        case 2:
                            {
                                string name;
                                Console.WriteLine("Введите название книги");
                                name = Console.ReadLine();
                                if (books.FindOne(f => f.Name == name) != null)
                                {
                                    books.FindOne(f => f.Name == name).PrintInfo();
                                }
                                else
                                {
                                    Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.WriteLine("Введите код книги");
                                x = Convert.ToInt32(Console.ReadLine());
                                if (books.FindOne(f => f.Code == x) != null)
                                {
                                    books.FindOne(f => f.Code == x).PrintInfo();
                                }
                                else
                                {
                                    Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;
                        case 4:
                            {
                                while (true)
                                {
                                    Console.WriteLine("Выберите тип книги\n");

                                    Console.WriteLine("1: {0}\n2: {1}\n3: {2}\n4: {3}\n5: {4}\n6: {5}\n7: {6}\n8: {7}\n9: {8}\n10: {9}\n",
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
                            break;
                        case 5:
                            {
                                Console.WriteLine("Введите имя автора");
                                string author_name = Console.ReadLine();
                                if (books.FindOne(f => f.Author_name == author_name) != null)
                                {
                                    foreach (Book book in books.FindAll().Where(w => w.Author_name == author_name))
                                    {
                                        book.PrintInfo();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                            }
                            break;
                        case 6:
                            {
                                Console.WriteLine("Введите дату публикации книги\n\tdd:MM:yyyy");
                                DateTime date = DateTime.Parse(Console.ReadLine());
                                if (books.FindOne(f => f.Publish_date == date) != null)
                                {
                                    foreach (Book book in books.FindAll().Where(w => w.Publish_date == date))
                                    {
                                        book.PrintInfo();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void GetAllBooks()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");
                    foreach (Book book in books.FindAll())
                    {
                        book.PrintInfo();
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
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    foreach (var reader in readers.FindAll().Where(w => w.IsBlock = true))
                    {
                        Console.WriteLine("______________________________\n");
                        Console.WriteLine("ID: {0}\nИмя :{1}\nЛогин :{2}\nПароль :{3}\nАдрес :{4}\nКонтакты :{5}\nEmail :{6}",
                            reader.Id,
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void BlockUserById(int reader_id)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");

                    Reader reader = readers.FindOne(f => f.Id == reader_id);
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

        public static AdminStatusOFAutorization LoginOn(string login, string password, out Administrator newAdmin)
        {
            newAdmin = null;

            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Administrator> admins = db.GetCollection<Administrator>("Administrator");
                    newAdmin = admins.FindOne(f => f.Login == login && f.Password == password);
                    if (newAdmin != null)
                        return AdminStatusOFAutorization.status01;
                    else
                        return AdminStatusOFAutorization.status02;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return AdminStatusOFAutorization.status03;
            }
        }
        public static int GetPunctMenu()
        {
            return Int32.Parse(Console.ReadLine());
        }
        public static void PrintFeedbacks()
        {
            foreach (Feedback feedback in ServiceAdministrator.feedbacks)
            {
                Console.WriteLine(string.Format("Номер выдачи :{0}\nНомер книги :{1}\nНазвание книги :{2}\nКод книги :{3}\nТип книги :{4}\nИмя должника: {5}\nАвтор книги: {6}\nДата выдачи :{7}\nДата возврата :{8}",
                    feedback.Issue_no,
                    feedback.S_No,
                    feedback.Name,
                    feedback.Code,
                    feedback.TypeBook,
                    feedback.Borrow_name,
                    feedback.Author,
                    feedback.Issue_date,
                    feedback.Return_date));
            }
        }
        public static void PrintLogs()
        {
            foreach (string item in ServiceAdministrator.ReadersAndBooksRecords)
            {
                Console.WriteLine(item);
            }
        }
        public static void PrintProfile(Administrator admin)
        {
            Console.WriteLine("login {0} password {1}", admin.Login, admin.Password);
        }
    }
}
