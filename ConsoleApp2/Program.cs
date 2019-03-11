using Library.LIB.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum TypeMenu { AdminOrUser, Start, Admin, User }
    class Program
    {
        static void Main(string[] args)
        {
            if (!ServiceAdministrator.FirstAdminIsExist())
            {
                ServiceAdministrator.Registration(ServiceProgram.GetAdminInfoForRegist());
            }

            ServiceProgram.PrintMenu();

            switch (ServiceProgram.GetPunctMenu())
            {
                case 1:
                    {
                        Console.Clear();
                        ServiceProgram.AutorizationAdmin();
                        break;
                    }
                case 2:
                    {
                        ServiceProgram.PrintMenu(TypeMenu.Start);
                        switch (ServiceProgram.GetPunctMenu())
                        {
                            case 1:
                                {
                                    ServiceProgram.AutorizationUser();
                                }
                                break;
                            case 2:
                                {
                                    if (ServiceReader.Registration(ServiceProgram.GetUserInfoForRegist()))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Register ok");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Register error");
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
