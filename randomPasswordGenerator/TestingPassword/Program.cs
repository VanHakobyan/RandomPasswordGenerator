using RandomClass;
using System;

namespace TestingPassword
{
    class Program
    {

        static void Main()
        {

            string password = passRandom.RanPass(12);
            //12-Length password
            Console.WriteLine(password);


            Console.ReadKey();
        }
    }
}
