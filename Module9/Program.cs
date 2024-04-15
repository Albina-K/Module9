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
            Exception exception = new Exception("Произошло исключение");

            exception.Data.Add("Дата создания исклбчения", DateTime.Now);
            exception.HelpLink = "ссылка на внешний ресцрс www////";

            try
            {
                Console.WriteLine("Данный блок начал свою раблоту");

                Method2();
            }
            catch (Exception ex) when (ex is ArgumentNullException)
            {
                Console.WriteLine("Аргумент пустой");
            }
            catch (Exception ex) when (ex is FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

            finally
            {
                Console.WriteLine("Блок Finally сработал");
            }
        }
        static void Method1()
        {
            try
            {
                throw new Exception("Внутренне отключение");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        static void Method2()
        {
            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
        
       
    }

    public class MyException : Exception
    {
        public MyException()
        {

        }
        //В конструкторе MyException принимаем обязательный параметр string message и передаем его в конструктор унаследованного класса Exception.
        public MyException(string message) : base(message)
        {

        }
    }
    public class HumanException : ArgumentException
    {
        public HumanException(string _exceptionmessage) : base(_exceptionmessage)
        {

        }
    }

        public AccountDTO Authenticate(string _userName, string _password)
        {
            UserEntity findUser = base.FindByUsername(_userName);
            if (findUser is null) throw new HumanException("Пользователь не найдет в системе");
            if (findUser.password != _password) throw new HumanException("Пароль не корректный");
            return new AccountDTO(findUser, _token.Generate(findUser.id), roleRepository.FinndByUserID(findUser.id));

        }
    public class ExceptionHandler:ActionFilterAttribute, IExceptionFilter
    {
        readonly Error _error = new Error();
        public void OnException(ExceptionContext context)
        {
            _error.Write(context.Exception.ToString());
            string errorMessage = "Произошла непредвиденная ошибка в приложении. Администрация сайта уже бежит на помощь";
            if (context.Exception is HumanException) errorMessage = context.Exception.Message;
            context.Result = new BadRequestObjectResult(errorMessage);
        }
    }

    public class Program2
    {
        static int Division(int a, int b)
        {
            return a / b;
        }

        static void Main(string[] args)
        {
            try
            {
                int result = Division(7, 0);
                Console.WriteLine(result);
            }
            ///блок сработает только если будет исключение DivideByZeroException
            catch (System.DivideByZeroException)
            {
                Console.WriteLine("На ноль делить нельзя");
            }
            ///или
            catch (Exception ex)
            {
                if (ex is DivideByZeroException) Console.WriteLine("На ноль делить нельзя");
                else Console.WriteLine("Произошла непредвиденная ошибка в приложении");
            }

            finally
            {
                Console.WriteLine("Блок Finally сработал");
            }
          Console.ReadKey();
        }
    }

    public class Program3
    {
        public delegate int SumDelegate(int a, int b, int c);

        static void Main(string[] args)
        {
            SumDelegate sumDelegate = Sum;
            sumDelegate.Invoke(1, 2, 3);
            Console.ReadKey();
        }

        static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }
    }

    class Prigram4
    {
        delegate int Vic(int x, int y);

        static void Main(string[] args)
        {
            Vic vic = Vic1;
            int result = vic.Invoke(100, 30);

            Console.WriteLine(result);
        }

        static int Vic1(int x, int y)
        {
            return x - y;
        }
    }

    class Program5
    {
        public delegate void ShowDelegate();

        static void Main(string[] args)
        {
            ShowDelegate showDelegate = ShowMessage1;
            showDelegate += ShowMessage2;///добавление методов через делегат посредством +=
            showDelegate += ShowMessage3;
            showDelegate += ShowMessage4;
            showDelegate.Invoke();

            ///объединение делегатов
            ShowDelegate showdelegate1 = ShowMessage1;
            showdelegate1 += ShowMessage2;

            ShowDelegate showdelegate2 = ShowMessage3;
            showdelegate2 += ShowMessage4;

            ShowDelegate showdelegate3 = showdelegate1 + showdelegate2;

            showdelegate3.Invoke();
        }

        static void ShowMessage1()
        {

        }
        static void ShowMessage2()
        {

        }
        static void ShowMessage3()
        {

        }
        static void ShowMessage4()
        {

        }

    }

    class Program6
    {
        static void ShowMessage()
        {
            Console.WriteLine("Hello word");
        }

        static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        static bool CheckLength(string _row)
        {
            if (_row.Length > 3) return true;
            return false;
        }

       
        delegate void ShowMessageDelegate();
        delegate int SumDelegate(int a, int b, int c);
        delegate bool CheckLenghtDelegate(string _row);


        static void Main(string[] args)
        {
            ShowMessageDelegate showMessageDelegate = ShowMessage;
            showMessageDelegate.Invoke();
            ///заменяем на
            ///Action showMessageDelegate = ShowMessage;
            ///showMessageDelegate.Invoke();

            SumDelegate sumDelegate = Sum;
            int result = sumDelegate.Invoke(1, 30, 120);
            Console.WriteLine(result);
            ///заменяем на
            ///Func < int,int,int,int > sumDelegate = Sum; последний паарметр всегда выходной, а первые входные
            
            CheckLenghtDelegate checkLenghtDelegate = CheckLength;
            bool status = checkLenghtDelegate.Invoke("////");
            Console.WriteLine(status);
            ///заменяем на
            ///Predicate < string > checkLengthDelegate = CheckLength; возвращает только логическое значение и не более одного входного параметра
        }



    }

    public class AnonymousMethods
    {
        public delegate string GreetingsDelegate(string name);
        public static string Greetings(string name)
        {
            return "Привет" + name + "Добро пожаловать на скиллфактори";
        }

        static void Main(string[] args)
        {
            GreetingsDelegate gt = new GreetingsDelegate(AnonymousMethods.Greetings);
            string GreetingsMessage = gt.Invoke("Будущий гуру");
            Console.WriteLine(GreetingsMessage);
            Console.ReadKey();
        }

        ///or
        public delegate string GreetingsDelegate(string name);
        static void Main(string[] args)
        {
            GreetingsDelegate gt = delegate (string name)
            {
                return "Привет" + name + "Добро пожаловать на скиллфактори";
            };
            string GreetingsMessage = gt.Invoke("Pryanay");
            Console.WriteLine(GreetingsMessage);
            Console.ReadKey();
        }

        /// or        
        public class AnonymousMethods
        {
            public delegate string GreetingsDelegate(string name);

            static void Main(string[] args)
            {
                string Message = "добро пожаловать на SkillFactory!";
                GreetingsDelegate gd = delegate (string name)
                {
                    return "Привет @" + name + " " + Message;
                };
                string GreetingsMessage = gd.Invoke("Будущий гуру");
                Console.WriteLine(GreetingsMessage);
                Console.ReadKey();
            }
        }
    }
}
