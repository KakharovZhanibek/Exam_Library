using Library.LIB.Model;
using Library.LIB.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Library.LIB.Model.Model.ServiceAdministrator;
using static Library.LIB.Model.Model.ServiceReader;
using static Library.Program;

namespace Library
{
    public class ServiceProgram
    {
        private static Reader reader;
        private static Administrator admin;
        public ServiceProgram() { }
        public ServiceProgram(Reader u) { reader = u; }
        public ServiceProgram(Administrator a) { admin = a; }

        public static void PrintMenu(TypeMenu typeMenu = TypeMenu.AdminOrUser)
        {
            switch (typeMenu)
            {
                case TypeMenu.AdminOrUser:
                    {
                        Console.WriteLine("1.Администратор\n2.Пользователь");
                    }
                    break;
                case TypeMenu.Start:
                    {
                        Console.WriteLine("1.Войти\n2.Регистрация");
                    }
                    break;
                case TypeMenu.Admin:
                    {
                        Console.WriteLine("1. Поменять пароль");
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("2. Создать нового пользователя");
                        Console.WriteLine("3. Редактировать пользователя");
                        Console.WriteLine("4. Заблокировать пользователя");
                        Console.WriteLine("5. Вывести всех пользователей");
                        Console.WriteLine("6. Вывести всех заблокированных пользователей");
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("7. Добавить новую книгу");
                        Console.WriteLine("8. Найти книгу");
                        Console.WriteLine("9. Вывести все книги");
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("10. Просмотреть логи");
                        Console.WriteLine("11. Просмотреть историю возвратов книг");
                        Console.WriteLine("\n0. Выйти");
                    }
                    break;
                case TypeMenu.User:
                    {
                        Console.WriteLine("1. Поменять пароль");
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("2. Найти книгу");
                        Console.WriteLine("3. Взять книгу");
                        Console.WriteLine("4. Вернуть книгу");
                        Console.WriteLine("5. Мои книги");
                        Console.WriteLine("\n0. Выйти");
                    }
                    break;
                default:
                    break;
            }
        }

        public static int GetPunctMenu()
        {
            return Int32.Parse(Console.ReadLine());
        }
        public static Reader GetUserInfoForRegist()
        {
            Reader reader = new Reader();

            Console.Write("{0, -40} ", "Введите имя: ");
            reader.Name = Console.ReadLine();
            Console.Write("{0, -40} ", "Введите адрес: ");
            reader.Address = Console.ReadLine();
            Console.Write("{0, -40} ", "Введите контакты: ");
            reader.Contact = Console.ReadLine();
            Console.Write("{0, -40} ", "Введите email: ");
            reader.Email = Console.ReadLine();

            Console.WriteLine("--------------------------------");
            Console.Write("{0, -40} ", "Выберете логин:");
            reader.Login = Console.ReadLine();
            Console.Write("{0, -40} ", "Выберете пароль:");
            reader.Password = Console.ReadLine();
            Console.WriteLine("--------------------------------");

            reader.IsBlock = false;

            return reader;
        }

        public static Administrator GetAdminInfoForRegist()
        {
            Administrator admin = new Administrator();
            Console.Write("{0, -40} ", "Введите имя: ");
            admin.Name = Console.ReadLine();

            Console.WriteLine("--------------------------------");
            Console.Write("{0, -40} ", "Выберете логин:");
            admin.Login = Console.ReadLine();
            Console.Write("{0, -40} ", "Выберете пароль:");
            admin.Password = Console.ReadLine();
            Console.WriteLine("--------------------------------");

            return admin;
        }

        public static void AutorizationUser()
        {
            int count = 3;

            do
            {
                Console.Write("Введите логин: ");
                reader = new Reader();

                reader.Login = Console.ReadLine();
                Console.Write("Введите Пароль: ");
                reader.Password = Console.ReadLine();

                if (ServiceReader.UserIsExist(reader.Login))
                {
                    UserStatusOFAutorization status = ServiceReader.LoginOn(reader.Login, reader.Password, out reader);

                    if (status == UserStatusOFAutorization.status02)
                    {
                        count--;
                        Console.WriteLine("У вас осталось {0} попыток", count);
                    }
                    else if (status == UserStatusOFAutorization.status01)
                    {
                        string exit="";
                        do
                        {
                            Console.Clear();
                            SetConsoleColor(string.Format("Добро пожаловать, {0}", reader.Name), ConsoleColor.Green);
                            PrintMenu(TypeMenu.User);
                            switch (GetPunctMenu())
                            {
                                case 1:
                                    {
                                        ServiceReader.ChangePassword(reader);
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 2:
                                    {
                                        ServiceReader.SearchBook();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;

                                case 3:
                                    {
                                        ServiceReader.Issue(reader);
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 4:
                                    {
                                        ServiceReader.Return(reader);
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 5:
                                    {
                                        reader.PrintMyBooks();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                case 0:
                                    {
                                        exit = "Back";
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                            }
                        } while (exit != "Back");

                        break;
                    }
                    else
                    {
                        SetConsoleColor("Ошибка авторизации", ConsoleColor.Red);
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    SetConsoleColor("Такого пользователя не существует", ConsoleColor.Red);
                }
            } while (count > 0);

            if (count == 0)
            {
                ServiceAdministrator.BlockUserByLogin(reader.Login);
                Console.Clear();
                SetConsoleColor("Вы заблокированы", ConsoleColor.Red);
            }
        }

        public static void AutorizationAdmin()
        {
            Console.Write("Введите логин: ");
            admin = new Administrator();

            admin.Login = Console.ReadLine();
            Console.Write("Введите Пароль: ");
            admin.Password = Console.ReadLine();

            if (ServiceAdministrator.UserIsExist(admin.Login))
            {
                AdminStatusOFAutorization status = ServiceAdministrator.LoginOn(admin.Login, admin.Password, out admin);

                if (status == AdminStatusOFAutorization.status02)
                {
                    Console.WriteLine("Неправильно введены логин или пароль");
                }
                else if (status == AdminStatusOFAutorization.status01)
                {
                    string exit = "";
                    do
                    {
                        Console.Clear();
                        SetConsoleColor(string.Format("Добро пожаловать, {0}", admin.Name), ConsoleColor.Green);
                        PrintMenu(TypeMenu.Admin);
                        switch (GetPunctMenu())
                        {
                            case 777:
                                {
                                    ServiceAdministrator.PrintProfile(admin);
                                    Thread.Sleep(5000);
                                }
                                break;
                            case 1:
                                {
                                    ServiceAdministrator.ChangePassword(admin);
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 2:
                                {
                                    ServiceAdministrator.RegNewReader();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;

                            case 3:
                                {
                                    ServiceAdministrator.RedactReader();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 4:
                                {
                                    Console.WriteLine("1: Заблокировать по логину");
                                    Console.WriteLine("2: Заблокировать по ID");
                                    Console.WriteLine("3: Назад");
                                    switch (GetPunctMenu())
                                    {
                                        case 1:
                                            {
                                                Console.WriteLine("Введите логин");
                                                string login = Console.ReadLine();
                                                ServiceAdministrator.BlockUserByLogin(login);

                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            break;
                                        case 2:
                                            {
                                                Console.WriteLine("Введите ID");
                                                int id = Int32.Parse(Console.ReadLine());
                                                ServiceAdministrator.BlockUserById(id);

                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            break;
                                        case 3:
                                            {
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                    }
                                }
                                break;
                            case 5:
                                {
                                    ServiceAdministrator.GetAllReaders();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 6:
                                {
                                    ServiceAdministrator.GetAllBlockedReaders();
                                    Console.ReadKey();
                                    // Console.Clear();
                                }
                                break;
                            case 7:
                                {
                                    ServiceAdministrator.AddNewBook();
                                    Console.ReadKey();
                                    // Console.Clear();
                                }
                                break;
                            case 8:
                                {
                                    ServiceAdministrator.FindBook();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 9:
                                {
                                    ServiceAdministrator.GetAllBooks();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 10:
                                {
                                    ServiceAdministrator.PrintLogs();
                                    Console.ReadKey();
                                    // Console.Clear();
                                }
                                break;
                            case 11:
                                {
                                    ServiceAdministrator.PrintFeedbacks();
                                    Console.ReadKey();
                                    //Console.Clear();
                                }
                                break;
                            case 0:
                                {
                                    exit = "Back";
                                    Console.ReadKey();
                                    //Console.Clear();
                                    break;
                                }
                        }
                    } while (exit != "Back");
                }
                else
                {
                    SetConsoleColor("Ошибка авторизации", ConsoleColor.Red);
                }
            }
            else
            {
                Console.Clear();
                SetConsoleColor("Такого пользователя не существует", ConsoleColor.Red);
            }
        }

        private static void SetConsoleColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
