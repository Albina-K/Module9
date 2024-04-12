using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class MyException:Exception
    {
        public MyException() 
        {
        
        }
        //В конструкторе MyException принимаем обязательный параметр string message и передаем его в конструктор унаследованного класса Exception.
        public MyException(string message): base(message)
        {

        }


        public class HumanException: ArgumentException
        {
            public HumanException(string _exceptionmessage): base(_exceptionmessage)
            {

            }
        }

        public AccountDTO Authenticate(string _userName, string _password)
        {
            UserEntity findUser = base.FindByUsername(_userName);

        }
    }
}
