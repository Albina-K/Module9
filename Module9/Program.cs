using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static Module9.Program7;

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

        //or
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

        // or        
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
    public class Program7
    {
        public delegate bool EligibleToPromotion(Employee EmployeeToPromotion);
        static void Main(string[] args)
        {
            Employee empl1 = new Employee()
            {
                ID = 55,
                Name = "Alexey",
                Experience = 5,
                Salary = 20000
            };
            Employee empl2 = new Employee()
            {
                ID = 545,
                Name = "A4exey",
                Experience = 45,
                Salary = 20400
            };
            Employee empl3 = new Employee()
            {
                ID = 543,
                Name = "A4ex3ey",
                Experience = 435,
                Salary = 203400
            };

            List<Employee> lstEmployees = new List<Employee>();///объявили пустой список сотрудников
            lstEmployees.Add(empl1);///добавляем в пустой список созданных ранее сотрудников
            lstEmployees.Add(empl2);
            lstEmployees.Add(empl3);

            EligibleToPromotion eligibleToPromotion = Promote; ///в наш делегат присваеваем метод Promote
            Employee.PromoteEmpoyee(lstEmployees, eligibleToPromotion);///вызываем метод PromoteEmpoyee, на вход ожидает список сотрудников который мы уже сделали, второй паарметр это делегат, котрый мы только что объявили
            Console.ReadKey();
        }
        public static bool Promote(Employee employee) ///возвращает значение подходит ли сотрудник под условие повышения
        {
            if (employee.Salary > 10000) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    // класс сотрудников  
    
    public class Employee
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Salary { get; set; }

        //метод выводит повышенных сотрудников
        public static void PromoteEmpoyee(List<Employee> listEmployees, EligibleToPromotion IsEmploteeEligible)//на вход требует список сотрудников и воторой параметр это делегат
        {
            ///цикл перебор каждого сотрудника из входного списка
            foreach(Employee employee in listEmployees) 
            {
                if (IsEmploteeEligible(employee))///условие, если попадает под условие повышения мы выводим данного сотрудника в консоль
                {
                    Console.WriteLine("Employee {0} Promoted,", employee.Name);
                }
            }
        }
    }

    //лямбда оператор во время анонимного метода
    class Program8
    {
        delegate void ShowMessageDelegate(string _message);
        static void Main(string[] args)
        {
            ShowMessageDelegate showMessageDelegate = (string _message) =>
            {
                Console.WriteLine(_message);
            };
            showMessageDelegate.Invoke("Hello World!");
            Console.Read();
        }

        static void ShowMessage(string _message)
        {
            Console.WriteLine(_message);
        }
    }

    class Program8_0///то что было до лямбда оператора
    {
        delegate void ShowMessageDelegate(string _message);
        static void Main(string[] args)
        {
            ShowMessageDelegate showMessageDelegate = ShowMessage;
            showMessageDelegate.Invoke("Hello World!");
            Console.Read();
        }

        static void ShowMessage(string _message)
        {
            Console.WriteLine(_message);
        }
    }

    //лямбда оператор во время анонимного метода
    class Program9
    {
        delegate int RandomNumberDelegate();
        static void Main(string[] args)
        {
            RandomNumberDelegate randomNumberDelegate = () =>
            {
                return new Random().Next(0, 100);
            };
            int result = randomNumberDelegate.Invoke();
            Console.WriteLine(result);
            Console.Read();
        }
    }

    class Program9_0///было до лямбда оператора
    {
        delegate int RandomNumberDelegate();
        static void Main(string[] args)
        {
            RandomNumberDelegate randomNumberDelegate = RandomNumber;
            int result = randomNumberDelegate.Invoke();
            Console.WriteLine(result);
            Console.Read();
        }

        static int RandomNumber()
        {
            return new Random().Next(0, 100);
        }
    }
    //ковариативность это когда мы можем объявлять делегат и назначать ему методы другой сигнатуры, но которые являются производными от основного метода.
    class Car
    {
        public string Model { get; set; }
    }
    class BMW : Car 
    { 
    
    }
    class Program10
    {
        delegate Car CarDelegate(string name);
        static void Main(string[] args)
        {
            CarDelegate carDelegate;
            carDelegate = BuildBMW; // ковариантность
            Car c = carDelegate("X6");
            Console.WriteLine(c.Model);
            Console.Read();
        }
        private static BMW BuildBMW(string model)
        {
            return new BMW { Model = model };
        }
    }
    public class Program11
    {
        delegate void BwmInfo(BMW bwm);
        static void Main(string[] args)
        {
            BwmInfo bmwInfo = GetCarInfo; // контравариантность
            BMW bwm = new BMW
            {
                Model = "X6"
            };
            bmwInfo(bwm);
            Console.Read();
        }

        private static void GetCarInfo(Car p)
        {
            Console.WriteLine(p.Model);
        }
    }

    class Program12
    {
        public delegate Animal HandLerMethod(); //делегат имеем класс возращаемого знаечия энимал

        public delegate void DogInfo(Dog dog); //на вход класс дог
        public static Animal AnimalHandler()
        {
            return null;
        }

        public static Dog DogHandler()
        {
            return null;
        }
        static void Main(string[] args) 
        {
            DogInfo dog = GetAnimalInfo;
            dog.Invoke(new Dog());//через метод dog.Invoke присваиваем объект класса

            HandLerMethod handLerMethod = AnimalHandler;

            HandLerMethod handLerDog = DogHandler;

            Console.Read();
        }

        public static void GetAnimalInfo(Animal p)
        {
            Console.WriteLine(p.GetType()); //в консольное приложение выведится тип нашего класса который поступил в метод GetAnimalInfo
        }
    }

    class Animal
    {

    }

    class Dog : Animal
    {

    }
