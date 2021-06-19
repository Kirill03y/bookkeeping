using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Бухгалтерія
{
    /* Курсова робота студента группи К20-3 Абрамова Кирила 
     * 
     * Бухгалтерія. Створюємо структуру працівник, яка складається з:
     *   - Особистого номеру
     *   - ПІБ
     *   - Посада
     *   - Оклад
     *   - дата працевлаштування.
     *   - Створюємо структуру Неробочий день, яка складається з:
     *   - Пояснення чому день не робочий(наприклад назва свята)
     *   - Дата 
     *   Всі дні у місяці крім неробочих и субботи та неділі рахуемо за вихідні.
     *   Програма повинна вміти здійснення по структурам “Працівник” і “Неробочий день” 4 базових операцій (створення, редагування, видалення, пошук).
     *   Повинна бути можливість сформувати помісячний звіт, де можно побачити:
     *   - Оклад працівника
     *   - кількість відпрацьованих днів у місяці
     *   - Зарплатню за місяц на основі кількості відроблених днів
     *   - Розмір воєнного сбору (1.5%)
     *   - Розмір налогу на прибуток фізічних осіб
     *   - Розмір зарплатні без налогів.  
     */


    [Serializable]
    struct worker
    {
        public worker(long number, string name, string position, int salary, DateTime dateOfEmployment)
        {
            this.number = number;
            this.name = name;
            this.position = position;
            this.salary = salary;
            this.dateOfEmployment = dateOfEmployment;
        }

        public long number;
        public string name;
        public string position;
        public int salary;
        public DateTime dateOfEmployment;

    }

    [Serializable]
    struct NonWorkingDay
    {
        public NonWorkingDay(string reason, DateTime day)
        {
            this.reason = reason;
            this.day = day;
        }


        public string reason;
        public DateTime day;
    }

    struct days2021 // для отчета
    {
        public days2021(int numberMounth, int countDays, int countDaysOff, int missedDays, List<NonWorkingDay> nwd)
        {
            this.numberMounth = numberMounth;
            this.countDays = countDays;
            
            int n = 0;
            foreach (var item in nwd)
            {
                if (item.day.Month == numberMounth)
                {
                    n++;
                }
            }
            countNonWorkingDays = n; //количество праздников в месяце

            workingDays = countDays - (countDaysOff + countNonWorkingDays);
            workedDays = workingDays - missedDays;

        }
        public int numberMounth;
        public int countDays;     
        public int countNonWorkingDays;
        public int workedDays; //отработанные дни
        public int workingDays; //рабочие дни
    }

    class Program
    {

        static void CreateDay(List<NonWorkingDay> Date) //создает новый выходной
        {           
            Console.Write("Введiть пояснення чому день не робочий: ");
            string reason = Console.ReadLine();

            Console.Write("Введiть рiк: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Введiть мiсяць: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Введiть день: ");
            int day = int.Parse(Console.ReadLine());

            DateTime date = new DateTime(year, month, day);

            Date.Add(new NonWorkingDay(reason, date));
        }
        static void CreateWorker(List<worker> Worker) //создает нового работника
        {
            Console.Write("Введiть особистий номер працiвника: ");
            long number = long.Parse(Console.ReadLine());
            Console.Write("Введiть ПIБ працiвника: ");
            string workerName = Console.ReadLine();
            Console.Write("Введiть посаду працiвника: ");
            string position = Console.ReadLine();
            Console.Write("Введiть оклад працiвника: ");
            int salary = int.Parse(Console.ReadLine());
            Console.WriteLine("   Введiть дату працевлаштування: ");

            Console.Write("Введiть рiк: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Введiть мiсяць: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Введiть день: ");
            int day = int.Parse(Console.ReadLine());
            DateTime dateOfEmployment = new DateTime(year, month, day);

            Worker.Add(new worker(number, workerName, position, salary, dateOfEmployment));
        } 

        static int findReasonIndex(List<NonWorkingDay> Date, string reason) // находит индекс "почему день не рабочий"
        {
            for (int i = 0; i < Date.Count; i++)
            {
                if (Date[i].reason == reason)
                {
                    return i;
                }
            }
            return 0;
        }
        static int findDayIndex(List<NonWorkingDay> Date, DateTime day) // находит индекс дня
        {
            for (int i = 0; i < Date.Count; i++)
            {
                if (Date[i].day == day)
                {
                    return i;
                }
            }
            return 0;
        }

        static int findName(List<worker> Worker, string name) //находит индекс имени
        {
            for (int i = 0; i < Worker.Count; i++)
            {
                if (Worker[i].name == name)
                {
                    return i;
                }
            }
            return 0;
        }
        static int findNumber(List<worker> Worker, long number) //находит индекс номера
        {
            for (int i = 0; i < Worker.Count; i++)
            {
                if (Worker[i].number == number)
                {
                    return i;
                }
            }
            return 0;
        }
        static int findPosition(List<worker> Worker, string position) //находит индекс должности
        {
            for (int i = 0; i < Worker.Count; i++)
            {
                if (Worker[i].position == position)
                {
                    return i;
                }
            }
            return 0;
        }
        static int findSalary(List<worker> Worker, int salary) //находит индекс оклада
        {
            for (int i = 0; i < Worker.Count; i++)
            {
                if (Worker[i].salary == salary)
                {
                    return i;
                }
            }
            return 0;
        }
        static int findDateOfEmployment(List<worker> Worker, DateTime day) // находит индекс дня
        {
            for (int i = 0; i < Worker.Count; i++)
            {
                if (Worker[i].dateOfEmployment == day)
                {
                    return i;
                }
            }
            return 0;
        }

        static void RemoveDay(List<NonWorkingDay> Date) //удаляет нерабочий день
        {
            Console.Write("Введiть назву для видалення:");
            string reason = Console.ReadLine();
            int index = findReasonIndex(Date, reason);
            Date.RemoveAt(index);
        }  
        static void RemoveWorker(List<worker> Worker) //удаляет работника
        {
            Console.WriteLine("Введiть ПIБ для видалення: ");
            string name = Console.ReadLine();
            int index = findName(Worker, name);
            Worker.RemoveAt(index);
        }

        static void PrintDays(List<NonWorkingDay> Date) //выводит все праздники
        {
            Console.WriteLine("\n----------- Неробочий день -----------");
            foreach (var item in Date)
            {
                Console.WriteLine(item.day.ToShortDateString() + "\t" + item.reason);
            }
        } 
        static void PrintDay(NonWorkingDay item)  //выводит один день
        {
            Console.WriteLine(item.day.ToShortDateString() + "\t" + item.reason);
        }


        static void PrintWorkers(List<worker> Worker) //выводит всех работников
        {
            Console.WriteLine("\n------------ Працiвник ------------");
            Console.WriteLine("\n\tПIБ:\t\t\tОсобистий номер:\tПосада:\t\tОклад:\tДата прцевлаштування");
            foreach (var item in Worker)
            {
                PrintForWorkers(item);
            }
        } 
        static void PrintWorker(worker item) //выводит одного работника
        {
            Console.WriteLine("\n\tПIБ:\t\t\tОсобистий номер:\tПосада:\t\tОклад:\tДата прцевлаштування");
            PrintForWorkers(item);
        }
        static void PrintForWorkers(worker item) //логика для вывода работников
        {
            Console.WriteLine($"{item.name}\t{item.number}\t\t{item.position}\t{item.salary}\t\t{item.dateOfEmployment.ToShortDateString()}");
        }



        static void Main(string[] args)
        {
            var workers = new List<worker>();

            workers.Add(new worker(380995615523, "Сергеев Николай Артёмович", "Администратор", 12000, new DateTime(2018, 5, 13)));
            workers.Add(new worker(380533696412, "Плотников Фёдор Егорович", "Официант", 9000, new DateTime(2012, 11, 7)));
            workers.Add(new worker(384562224565, "Абдулин Сергей Генадиевич", "Официант", 9000, new DateTime(2020, 2, 3)));
            workers.Add(new worker(381456872266, "Гордиенко Николай Сергеевич", "Дирекор ", 20000, new DateTime(2016, 9, 4)));
            workers.Add(new worker(385621117755, "Цветочкин Дмитрий Вадимович", "Шеф-повар", 15000, new DateTime(2013, 7, 15)));
            workers.Add(new worker(385695127756, "Горшкова Натилия Денисовна", "Официант", 10000, new DateTime(2021, 1, 3)));
            workers.Add(new worker(380564128594, "Абдулин Валерий Генадиевич", "Повар   ", 11000, new DateTime(2020, 2, 3)));

            var daysOff = new List<NonWorkingDay>();

            daysOff.Add(new NonWorkingDay("Новий рiк", new DateTime(2021, 01, 01)));
            daysOff.Add(new NonWorkingDay("Рiздво Христове", new DateTime(2021, 01, 07)));
            daysOff.Add(new NonWorkingDay("Мiжнародний жiночий день", new DateTime(2021, 03, 08)));            
            daysOff.Add(new NonWorkingDay("День працi(переноситься)", new DateTime(2021, 05, 03)));
            daysOff.Add(new NonWorkingDay("Великдень(переноситься)", new DateTime(2021, 05, 04)));            
            daysOff.Add(new NonWorkingDay("День перемоги(переноситься)", new DateTime(2021, 05, 10)));         
            daysOff.Add(new NonWorkingDay("Трiйця(переноситься)", new DateTime(2021, 06, 21)));
            daysOff.Add(new NonWorkingDay("День Конституцiї", new DateTime(2021, 06, 28)));
            daysOff.Add(new NonWorkingDay("День незалежностi", new DateTime(2021, 08, 24)));
            daysOff.Add(new NonWorkingDay("День захисника", new DateTime(2021, 09, 14)));
            daysOff.Add(new NonWorkingDay("Рiздво Христове(переноситься)", new DateTime(2021, 12, 27)));


            BinaryFormatter formatter = new BinaryFormatter();
            // десериализация
            try
            {
                using (FileStream fs = new FileStream("Workers.dat", FileMode.OpenOrCreate))
                {
                    List<worker> deserilizePeople = (List<worker>)formatter.Deserialize(fs);

                    if (deserilizePeople != null)
                    {
                        workers = deserilizePeople;
                    }
                }
            }
            catch (Exception)
            {

            }

            try
            {
                using (FileStream fs2 = new FileStream("NonWorDays.dat", FileMode.OpenOrCreate))
                {
                    List<NonWorkingDay> deserilizeDays = (List<NonWorkingDay>)formatter.Deserialize(fs2);

                    if (deserilizeDays != null)
                    {
                        daysOff = deserilizeDays;
                    }
                }
            }
            catch (Exception)
            {

            }


            Console.WriteLine("Виконав: студент группи К20-3 Абрамов Кирило");
            Console.WriteLine("   *********** Бухгалтерiя **********\n");
            bool repeat = true;
            while (repeat)
            {
                try
                {       
                    Console.WriteLine("\nОберiть дiю: 1- Створення, 2- Редагування, 3- Видалення, 4- Пошук, 5- Сформувати помiсячний звiт, 6- Очистити консоль, 7- Вийти i зберегти");

                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1: //створення
                            Console.WriteLine("Оберiть структуру: 1- Працiвник, 2- Неробочий день");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1: //працiвник
                                    Console.WriteLine("\n\t----- Створення нового працiвника -----\n");
                                    CreateWorker(workers);
                                    break;
                                case 2: //неробочий день
                                    Console.WriteLine("\n\t----- Створення неробочого дня -----\n");
                                    CreateDay(daysOff);
                                    break;

                                default:
                                    Console.WriteLine("Помилка! Введiть 1-2");
                                    break;
                            }
                            break;

                        case 2: //редагування
                            Console.WriteLine("Оберiть структуру: 1- Працiвник, 2- Неробочий день");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1: //працiвник
                                    Console.WriteLine("\t----- Редагування -----");
                                    Console.WriteLine("Оберiть що редагувати: 1- ПIБ, 2- Особистий номер, 3- Посада, 4- Оклад, 5- Дата прцевлаштування");
                                    int edit = int.Parse(Console.ReadLine());
                                    switch (edit)
                                    {
                                        case 1:
                                            Console.Write("Введiть ПIБ для редагування: ");
                                            string name = Console.ReadLine();
                                            int ind = findName(workers, name);     //index
                                            Console.WriteLine("Введiть новi ПIБ");
                                            string newName = Console.ReadLine();
                                            worker newWorkerName = new worker(workers[ind].number, newName, workers[ind].position, workers[ind].salary, workers[ind].dateOfEmployment);
                                            workers.Insert(ind, newWorkerName);
                                            workers.RemoveAt(ind + 1);
                                            break;
                                        case 2:
                                            Console.Write("Введiть номер для редагування:");
                                            long number = long.Parse(Console.ReadLine());
                                            ind = findNumber(workers, number);
                                            Console.Write("Введiть новий номер: ");
                                            long newNumber = long.Parse(Console.ReadLine());
                                            worker newWorkerNumber = new worker(newNumber, workers[ind].name, workers[ind].position, workers[ind].salary, workers[ind].dateOfEmployment);
                                            workers.Insert(ind, newWorkerNumber);
                                            workers.RemoveAt(ind + 1);
                                            break;
                                        case 3:
                                            Console.Write("Введiть посаду для редагування: ");
                                            string position = Console.ReadLine();
                                            ind = findPosition(workers, position);
                                            Console.WriteLine("Введiть нову посаду");
                                            string newPosition = Console.ReadLine();
                                            worker newWorkerPosition = new worker(workers[ind].number, workers[ind].name, newPosition, workers[ind].salary, workers[ind].dateOfEmployment);
                                            workers.Insert(ind, newWorkerPosition);
                                            workers.RemoveAt(ind + 1);
                                            break;
                                        case 4:
                                            Console.WriteLine("Введiть оклад для редагування: ");
                                            int salary = int.Parse(Console.ReadLine());
                                            ind = findSalary(workers, salary);
                                            Console.WriteLine("Введiть новий оклад");
                                            int newSalary = int.Parse(Console.ReadLine());
                                            worker newWorkerSalary = new worker(workers[ind].number, workers[ind].name, workers[ind].position, newSalary, workers[ind].dateOfEmployment);
                                            workers.Insert(ind, newWorkerSalary);
                                            workers.RemoveAt(ind + 1);
                                            break;
                                        case 5:
                                            Console.WriteLine("Введiть дату для редагування");                                            
                                            Console.Write("Введiть рiк: ");
                                            int year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            int month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            int day = int.Parse(Console.ReadLine());
                                            DateTime Date = new DateTime(year, month, day);
                                            ind = findDateOfEmployment(workers, Date);
                                            Console.WriteLine("Введiть нову дату: ");
                                            Console.Write("Введiть рiк: ");
                                            year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            day = int.Parse(Console.ReadLine());
                                            DateTime newDate = new DateTime(year, month, day);
                                            worker newWorkerDate = new worker(workers[ind].number, workers[ind].name, workers[ind].position, workers[ind].salary, newDate);
                                            workers.Insert(ind, newWorkerDate);
                                            workers.RemoveAt(ind + 1);
                                            break;
                                        default:
                                            Console.WriteLine("Помилка!!! Введiть 1-5");
                                            break;
                                    }
                                    break;
                                case 2: //неробочий день

                                    Console.WriteLine("\t----- Редагування -----");
                                    Console.WriteLine("1- редагувати назву, 2- редагувати дату");
                                    edit = int.Parse(Console.ReadLine());
                                    switch (edit)
                                    {
                                        case 1:                                           
                                            Console.Write("Введiть назву для редагування: ");
                                            string reason = Console.ReadLine();
                                            int index = findReasonIndex(daysOff, reason);
                                            Console.WriteLine("Введiть нову назву");
                                            string newReason = Console.ReadLine();

                                            NonWorkingDay newName = new NonWorkingDay(newReason, daysOff[index].day);
                                            daysOff.Insert(index, newName);
                                            daysOff.RemoveAt(index + 1);
                                            break;

                                        case 2:
                                            Console.WriteLine("Введiть дату для редагування: ");
                                            Console.Write("Введiть рiк: ");
                                            int year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            int month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            int day = int.Parse(Console.ReadLine());
                                            DateTime date = new DateTime(year, month, day);

                                            index = findDayIndex(daysOff, date);

                                            Console.WriteLine("Введiть нову дату: ");
                                            Console.Write("Введiть рiк: ");
                                            year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            day = int.Parse(Console.ReadLine());
                                            DateTime newDate = new DateTime(year, month, day);

                                            NonWorkingDay newDay = new NonWorkingDay(daysOff[index].reason, newDate);
                                            daysOff.Insert(index, newDay);
                                            daysOff.RemoveAt(index + 1);
                                            break;
                                    }
                                    break;

                                default:
                                    Console.WriteLine("Помилка! Введiть 1-2");
                                    break;
                            }
                            break;

                        case 3: //видалення
                            Console.WriteLine("Оберiть структуру: 1- Працiвник, 2- Неробочий день");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1: //працiвник
                                    Console.WriteLine("\t----- Видалення -----");
                                    RemoveWorker(workers);
                                    break;
                                case 2: //неробочий день

                                    Console.WriteLine("\t----- Видалення -----");
                                    RemoveDay(daysOff);

                                    break;

                                default:
                                    Console.WriteLine("Помилка! Введiть 1-2");
                                    break;
                            }
                            break;

                        case 4: //пошук
                            Console.WriteLine("Оберiть структуру: 1- Працiвник, 2- Неробочий день");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1: //працiвник
                                    Console.WriteLine("Оберiть вид пошуку: 1- за ПIБ, 2- за особистим номером, 3- за посадою, 4- за окладом, 5- за датою працевлаштування, 6- Вивести всех працiвникiв, 7- Пошук за декiлькома параметрами");
                                    
                                    switch (int.Parse(Console.ReadLine()))
                                    {
                                        case 1:
                                            Console.Write("Введiть ПIБ: ");
                                            string name = Console.ReadLine();
                                            foreach (var item in workers)
                                            {
                                                if (item.name == name)
                                                {
                                                    PrintWorker(item);
                                                }
                                            }
                                            break;
                                        case 2:
                                            Console.Write("Введiть особистий номер: ");
                                            long number = long.Parse(Console.ReadLine());
                                            foreach (var item in workers)
                                            {
                                                if (item.number == number)
                                                {
                                                    PrintWorker(item);
                                                }
                                            }
                                            break;
                                        case 3:
                                            Console.Write("Введiть посаду: ");
                                            string position = Console.ReadLine();
                                            foreach (var item in workers)
                                            {
                                                if (item.position == position)
                                                {
                                                    PrintWorker(item);
                                                }
                                            }
                                            break;
                                        case 4:
                                            Console.Write("Введть оклад: ");
                                            int salary = int.Parse(Console.ReadLine());
                                            foreach (var item in workers)
                                            {
                                                if (item.salary == salary)
                                                {
                                                    PrintWorker(item);
                                                }
                                            }
                                            break;
                                        case 5:
                                            Console.WriteLine("Введiть дату працевлаштування: ");
                                            Console.Write("Введiть рiк: ");
                                            int year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            int month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            int day = int.Parse(Console.ReadLine());
                                            DateTime Date = new DateTime(year, month, day);
                                            foreach (var item in workers)
                                            {
                                                if (item.dateOfEmployment == Date)
                                                {
                                                    PrintWorker(item);
                                                }
                                            }
                                            break;
                                        case 6:
                                            PrintWorkers(workers);
                                            break;
                                        case 7:
                                            {
                                                var FindDepth1 = new List<worker>();
                                                foreach (var item in workers) //Поскольку списки относятся к класам, нельзя просто написать FileDepth1 = workers(так мы передадим ссылку, а нужно копировать)
                                                {
                                                    FindDepth1.Add(item);
                                                }                                               

                                                var FindDepth2 = new List<worker>();

                                                bool repeatFind = true;
                                                while (repeatFind)
                                                {

                                                    Console.WriteLine("\nОберiть параметр для пошуку: 1- за ПIБ, 2- за особистим номером, 3- за посадою, 4- за окладом, 5- за датою працевлаштування, 6- Повернутися до головного меню");
                                                    switch (int.Parse(Console.ReadLine()))
                                                    {
                                                        case 1:
                                                            Console.Write("Введiть ПIБ: ");
                                                            name = Console.ReadLine();
                                                            foreach (var item in FindDepth1)
                                                            {
                                                                if (item.name == name)
                                                                {
                                                                    FindDepth2.Add(item);
                                                                }
                                                            }
                                                            break;
                                                        case 2:
                                                            Console.Write("Введiть особистий номер: ");
                                                            number = long.Parse(Console.ReadLine());
                                                            foreach (var item in FindDepth1)
                                                            {
                                                                if (item.number == number)
                                                                {
                                                                    FindDepth2.Add(item);
                                                                }
                                                            }
                                                            break;
                                                        case 3:
                                                            Console.Write("Введiть посаду: ");
                                                            position = Console.ReadLine();
                                                            foreach (var item in FindDepth1)
                                                            {
                                                                if (item.position == position)
                                                                {
                                                                    FindDepth2.Add(item);
                                                                }
                                                            }
                                                            break;
                                                        case 4:
                                                            Console.Write("Введть оклад: ");
                                                            salary = int.Parse(Console.ReadLine());
                                                            foreach (var item in FindDepth1)
                                                            {
                                                                if (item.salary == salary)
                                                                {
                                                                    FindDepth2.Add(item);
                                                                }
                                                            }
                                                            break;
                                                        case 5:
                                                            Console.WriteLine("Введiть дату працевлаштування: ");
                                                            Console.Write("Введiть рiк: ");
                                                            year = int.Parse(Console.ReadLine());
                                                            Console.Write("Введiть мiсяць: ");
                                                            month = int.Parse(Console.ReadLine());
                                                            Console.Write("Введiть день: ");
                                                            day = int.Parse(Console.ReadLine());
                                                            Date = new DateTime(year, month, day);
                                                            foreach (var item in FindDepth1)
                                                            {
                                                                if (item.dateOfEmployment == Date)
                                                                {
                                                                    FindDepth2.Add(item);
                                                                }
                                                            }
                                                            break;
                                                        case 6:
                                                            repeatFind = false;
                                                            break;
                                                    }

                                                    PrintWorkers(FindDepth2);
                                                    FindDepth1.Clear();
                                                    foreach (var item in FindDepth2)
                                                    {
                                                        FindDepth1.Add(item);
                                                    }
                                                    FindDepth2.Clear();
                                                }
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("Помилка! Введiть 1-6");
                                            
                                            break;
                                    } //поиск по работникам

                                    break;
                                case 2: //неробочий день
                                    Console.WriteLine("Оберiть вид пошуку: 1- за назвою дня, 2- за датою, 3- вивести всi неробочi днi");

                                    switch (int.Parse(Console.ReadLine()))
                                    {
                                        case 1:
                                            Console.Write("Введiть назву дня: ");
                                            string name = Console.ReadLine();
                                            foreach (var item in daysOff)
                                            {
                                                if (item.reason == name)
                                                {
                                                    PrintDay(item);
                                                }
                                            }
                                            break;
                                        case 2:
                                            Console.WriteLine("Введiть дату: ");
                                            Console.Write("Введiть рiк: ");
                                            int year = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть мiсяць: ");
                                            int month = int.Parse(Console.ReadLine());
                                            Console.Write("Введiть день: ");
                                            int day = int.Parse(Console.ReadLine());
                                            DateTime Date = new DateTime(year, month, day);
                                            foreach (var item in daysOff)
                                            {
                                                if (item.day == Date)
                                                {
                                                    PrintDay(item);
                                                }
                                            }
                                            break;
                                        case 3:
                                            PrintDays(daysOff);
                                            break;
                                        default:
                                            Console.WriteLine("Помилка! Введiть 1-3");
                                            break;
                                    }

                                    break;

                                default:
                                    Console.WriteLine("Помилка! Введiть 1-2");
                                    break;
                            }
                            break;

                        case 5: //сформувати звiт
                            Console.Write("Введiть ПIБ працiвника: ");
                            string workerName = Console.ReadLine();
                            int i = findName(workers, workerName);

                            Console.WriteLine("Введiть номер мiсяця");
                            int monthInd = int.Parse(Console.ReadLine()) - 1;
                            Console.WriteLine("Введiть кiлькiсть пропускiв за мiсяць");
                            int missedDays = int.Parse(Console.ReadLine());


                            var WorkingDays2021 = new List<days2021>();
                            WorkingDays2021.Add(new days2021(1, 31, 10, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(2, 28, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(3, 31, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(4, 30, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(5, 30, 10, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(6, 30, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(7, 31, 9, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(8, 31, 9, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(9, 30, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(10, 31, 10, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(11, 30, 8, missedDays, daysOff));
                            WorkingDays2021.Add(new days2021(12, 31, 8, missedDays, daysOff));

                            double salaryInADay = workers[i].salary / WorkingDays2021[monthInd].workingDays; //зарплата за день в данном месяце
                            double salaryWithoutTax = salaryInADay * WorkingDays2021[monthInd].workedDays; //зарплата за месяц без налогов
                            double firstTax = salaryWithoutTax / 100 * 1.5; //военный сбор
                            double secoundTax = salaryWithoutTax / 100 * 18; //подоходный налог
                            double finalSalary = salaryWithoutTax - firstTax - secoundTax; // зарплата с налогами

                            Console.WriteLine($"Оклад працiвника: {workers[i].salary}");
                            Console.WriteLine($"Кiлькiсть вiдпрацьованих днiв у мiсяцi: {WorkingDays2021[monthInd].workedDays}");
                            Console.WriteLine($"Зарплатня за мiсяць: {String.Format("{0:f1}", finalSalary)}");
                            Console.WriteLine($"Розмiр воєнного сбору: {String.Format("{0:f1}", firstTax)}");
                            Console.WriteLine($"Розмiр налогу на прибуток фiзичних осiб: {String.Format("{0:f1}", secoundTax)}");
                            Console.WriteLine($"Розмiр зарплатнi без налогiв: {String.Format("{0:f1}", salaryWithoutTax)}");
                            break;

                        case 6: //очистити консоль

                            Console.Clear();
                            Console.WriteLine("Виконав: студент группи К20-3 Абрамов Кирило");
                            Console.WriteLine("   *********** Бухгалтерiя **********\n");
                            break;

                        case 7:

                            repeat = false;
                            break;
                        default:
                            Console.WriteLine("Помилка! Введіть 1-6");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Помилка ...");
                }             
            }

            using (FileStream fs = new FileStream("Workers.dat", FileMode.OpenOrCreate))
            {

                formatter.Serialize(fs, workers);

            }
            using (FileStream fs2 = new FileStream("NonWorDays.dat", FileMode.OpenOrCreate))
            {

                formatter.Serialize(fs2, daysOff);

            }
            Console.WriteLine("Збережено");
        }
    }
}
