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
        public enum StatusOFAutorization { status01, status02, status03 }

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
            using (var db = new LiteDatabase(@"Library.db"))
            {
                LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                Reader reader = readers.FindOne(u => u.Login == login);
                if (reader != null)
                    return true;
                else return false;
            }
        }

        public static StatusOFAutorization LoginOn(string login, string password, out Reader newReader)
        {
            newReader = null;

            try
            {
                using (var db = new LiteDatabase(@"Library.db"))
                {
                    LiteCollection<Reader> readers = db.GetCollection<Reader>("Reader");
                    newReader = readers.FindOne(f => f.Login == login && f.Password == password);
                    if (newReader != null)
                        return StatusOFAutorization.status01;
                    else
                        return StatusOFAutorization.status02;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusOFAutorization.status03;
            }
        }

    }
}
