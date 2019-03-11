using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model.Model
{
    public class ServiceReader
    {
        public enum UserStatusOFAutorization { status01, status02, status03 }

        public static bool Registration(Reader reader)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
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

        public static bool UserIsExist(string login)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static void SearchBook()
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");
                    Console.WriteLine("Выберите по какому свойству хотите искать книгу");
                    Console.WriteLine("1: Номер\n2: Название\n3: Код\n4: Тип\n5: Имя автора\n6: Дата публикации\n");
                    int x = 0;
                    x = Convert.ToInt32(Console.ReadLine());
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

                                    //book.TypeBook = (TypeBook)Convert.ToInt32(Console.ReadLine());
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

        public static Book SearchByNo()
        {
            Book R = null;
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");


                    int x;
                    Console.WriteLine("Введите номер книги");

                    x = Convert.ToInt32(Console.ReadLine());

                    if (books.FindOne(f => f.S_no == x) != null)
                    {
                        books.FindOne(f => f.S_no == x).PrintInfo();

                        while (true)
                        {
                            Console.WriteLine(" \nВы выбираете эту книгу?/n/t/tY/N");
                            char choice;

                            choice = Convert.ToChar(Console.ReadKey());

                            if (choice == 'y' || choice == 'Y')
                            {
                                return books.FindOne(f => f.S_no == x);
                            }
                            else if (choice == 'n' || choice == 'N')
                            {
                                Console.ReadKey();
                                Console.Clear();
                                return R;
                                //break;
                            }
                            return R;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                        Console.ReadKey();
                        Console.Clear();
                        return R;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return R;
            }
        }

        public static Book SearchByName()
        {
            Book R = null;
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");


                    string name;

                    Console.WriteLine("Введите название книги");
                    name = Console.ReadLine();
                    if (books.FindOne(f => f.Name == name) != null)
                    {
                        books.FindOne(f => f.Name == name).PrintInfo();

                        while (true)
                        {
                            Console.WriteLine(" \nВы выбираете эту книгу?/n/t/tY/N");
                            char choice;

                            choice = Convert.ToChar(Console.ReadKey());
                            if (choice == 'y' || choice == 'Y')
                            {
                                return books.FindOne(f => f.Name == name);
                            }
                            else if (choice == 'n' || choice == 'N')
                            {
                                Console.ReadKey();
                                Console.Clear();
                                return R;
                                //break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Такая книга отсутствует в библиотеке.\nPress any key...");
                        Console.ReadKey();
                        Console.Clear();
                        return R;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return R;
            }
        }
        
        public static void ChangePassword(Reader reader)
        {
            Console.WriteLine("Введите старый пароль");
            string password = Console.ReadLine();
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    while (true)

                    {
                        if (reader.Password == password)
                        {
                            Console.WriteLine("Введите новый пароль");
                            password = Console.ReadLine();
                            reader.Password = password;
                            readers.Update(reader);
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

        public static void Issue(Reader reader)
        {
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    while (true)
                    {
                        Console.WriteLine("Введите номер или название книги которую хотите взять. \tExit");
                        string IdOrName = Console.ReadLine();

                        int no;
                        Book temp = null;
                        if (Int32.TryParse(IdOrName, out no) && books.FindOne(f => f.S_no == no) != null)
                        {
                            temp = books.FindOne(f => f.S_no == no);
                            temp.PrintInfo();
                            while (true)
                            {
                                Console.WriteLine(" \nВы выбираете эту книгу?\n\t\tY/N");
                                char choice;

                                choice = Convert.ToChar(Console.ReadLine());
                                if (choice == 'y' || choice == 'Y')
                                {
                                    reader.MyBooks.Add(temp, DateTime.Now.AddDays(14));
                                    temp.Amount--;
                                    books.Update(temp);
                                    readers.Update(reader);

                                    ServiceAdministrator.ReadersAndBooksRecords.Add(string.Format("Пользователь {0} взял книгу \"{1}\"\nв {2} \t дата возврата {3}",
                                        reader.Login, temp.Name, DateTime.Now, DateTime.Now.AddDays(14)));

                                    break;
                                }
                                else if (choice == 'n' || choice == 'N')
                                {
                                    break;
                                }
                            }
                        }
                        else if (books.FindOne(f => f.Name == IdOrName) != null)
                        {
                            temp = books.FindOne(f => f.Name == IdOrName);
                            temp.PrintInfo();

                            while (true)
                            {
                                Console.WriteLine(" \nВы выбираете эту книгу?/n/t/tY/N");
                                char choice;

                                choice = Convert.ToChar(Console.ReadLine());
                                if (choice == 'y' || choice == 'Y')
                                {
                                    reader.MyBooks.Add(temp, DateTime.Now.AddDays(14));
                                    temp.Amount--;
                                    books.Update(temp);
                                    readers.Update(reader);

                                    ServiceAdministrator.ReadersAndBooksRecords.Add(string.Format("Пользователь {0} взял книгу \"{1}\"\nв {2} \t дата возврата {3}",
                                        reader.Login, temp.Name, DateTime.Now, DateTime.Now.AddDays(14)));

                                    break;
                                }
                                else if (choice == 'n' || choice == 'N')
                                {
                                    break;
                                }
                            }
                        }
                        else if (IdOrName == "Exit")
                        {
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Книга с таким номером или названием не найдена.");
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

        public static void Return(Reader reader)
        {
            Random rnd = new Random();
            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Book> books = db.GetCollection<Book>("Book");
                    LiteCollection<Feedback> feedbacks = db.GetCollection<Feedback>("Feedback");
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");

                    foreach (KeyValuePair<Book, DateTime> book in reader.MyBooks)
                    {
                        book.Key.PrintInfo();
                        Console.WriteLine("Дата возврата {0}", book.Value);
                        if (DateTime.Now >= book.Value)
                        {
                            Console.WriteLine("Просрочено");
                        }
                        else
                        {
                            continue;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Введите номер или название книги которую хотите возвратить. \tExit");
                        string IdOrName = Console.ReadLine();

                        int no;
                        Book temp = null;
                        Int32.TryParse(IdOrName, out no);

                        if (reader.MyBooks.Keys.Where(w => w.S_no == no) != null)
                        {
                            temp = reader.MyBooks.Keys.Where(w => w.S_no == no).First();

                            reader.MyBooks.Remove(temp);
                            readers.Update(reader);

                            books.FindOne(f => f.S_no == temp.S_no).Amount++;
                            books.Update(books.FindOne(f => f.S_no == temp.S_no));

                            

                            Feedback feedback = new Feedback();
                            feedback.Issue_no = rnd.Next(10000, 99999);
                            feedback.S_No = temp.S_no;
                            feedback.Name = temp.Name;
                            feedback.Code = temp.Code;
                            feedback.TypeBook = temp.TypeBook;
                            feedback.Borrow_name = reader.Name;
                            feedback.Author = temp.Author_name;
                            DateTime issueDate;
                            reader.MyBooks.TryGetValue(temp, out issueDate);
                            feedback.Issue_date = issueDate;
                            feedback.Return_date = DateTime.Now;

                            ServiceAdministrator.ReadersAndBooksRecords.Add(string.Format("Пользователь {0} возвратил книгу \"{1}\"\nв {2} \t дата возврата {3}",
                                        reader.Login, temp.Name, DateTime.Now, DateTime.Now.AddDays(14)));

                            ServiceAdministrator.feedbacks.Add(feedback);

                            feedbacks.Insert(feedback);
                        }
                        else if (books.FindOne(f => f.Name == IdOrName) != null)
                        {
                            temp = reader.MyBooks.Keys.First(f => f.Name == IdOrName);

                            reader.MyBooks.Remove(temp);
                            readers.Update(reader);

                            books.FindOne(f => f.Name == temp.Name).Amount++;
                            books.Update(books.FindOne(f => f.Name == temp.Name));

                            Feedback feedback = new Feedback();
                            feedback.Issue_no = rnd.Next(10000, 99999);
                            feedback.S_No = temp.S_no;
                            feedback.Name = temp.Name;
                            feedback.Code = temp.Code;
                            feedback.TypeBook = temp.TypeBook;
                            feedback.Borrow_name = reader.Name;
                            feedback.Author = temp.Author_name;
                            DateTime issueDate;
                            reader.MyBooks.TryGetValue(temp, out issueDate);
                            feedback.Issue_date = issueDate;
                            feedback.Return_date = DateTime.Now;

                            ServiceAdministrator.ReadersAndBooksRecords.Add(string.Format("Пользователь {0} возвратил книгу \"{1}\"\nв {2} \t дата возврата {3}",
                                        reader.Login, temp.Name, DateTime.Now, DateTime.Now.AddDays(14)));

                            ServiceAdministrator.feedbacks.Add(feedback);

                            feedbacks.Insert(feedback);
                        }
                        else if (IdOrName == "Exit")
                        {
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Книга с таким номером или названием не найдена.");
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

        public static UserStatusOFAutorization LoginOn(string login, string password, out Reader newReader)
        {
            newReader = null;

            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    newReader = readers.FindOne(f => f.Login == login && f.Password == password);
                    if (newReader != null)
                        return UserStatusOFAutorization.status01;
                    else
                        return UserStatusOFAutorization.status02;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UserStatusOFAutorization.status03;
            }
        }

        public static int GetPunctMenu()
        {
            return Int32.Parse(Console.ReadLine());
        }
    }
}


