using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace PT_lab4
{
    static class HelperClass
    {
        public static List<Car> myCars = new List<Car>(){
                        new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                        new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                        new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                        new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                        new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                        new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                        new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                        new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                        new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
        };

        public static void runAll()
        {
            // zad 1
            LINQQuery();

            // zad 2
            ActionsWithDelegate();
        }

        private static void LINQQuery()
        {
            var q1 = from e in
                         (from c in myCars
                          where c.Model == "A6"
                          select new
                          {
                              engineType = c.Motor.Model == "TDI" ? "Diesel" : "Petrol",
                              hppl = c.Motor.HorsePower / c.Motor.Displacement
                          })
                     group e by e.engineType into cars
                     select new
                     {
                         engineType = cars.Key,
                         avgHppl = cars.Average(v => v.hppl)
                     } into selected
                     orderby selected.avgHppl descending
                     select selected;

                     

            foreach (var group in q1)
            {
                Console.WriteLine(group.engineType + ": " + group.avgHppl);
            }

            var q2 = myCars
                .Where(c => c.Model == "A6")
                .Select(c =>
                    new
                    {
                        engineType = c.Motor.Model == "TDI" ? "Diesel" : "Petrol",
                        hppl = c.Motor.HorsePower / c.Motor.Displacement
                    })
                .GroupBy(c => c.engineType)
                .Select(cars =>
                new
                {
                    engineType = cars.Key,
                    avgHppl = cars.Average(v => v.hppl)
                })
                .OrderByDescending(c => c.avgHppl);

            foreach (var group in q2)
            {
                Console.WriteLine(group.engineType + ": " + group.avgHppl);
            }


        }

        private static int CompareCarsEngineByHP(Car c1, Car c2)
        {
            return c1.Motor.CompareTo(c2.Motor);
        }

        private static void showMessageBox(Car c)
        {
            string message = c.ToString();
            string caption = "Cars with TDI engine";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            System.Windows.MessageBox.Show(message, caption, (MessageBoxButton)buttons);
        }

        private static void ActionsWithDelegate()
        { 
            List<Car> myCarsCopy = new List<Car>(myCars);

            Func<Car, Car, int> arg1 = CompareCarsEngineByHP;
            Predicate<Car> arg2 = c => (c.Motor.Model == "TDI");
            Action<Car> arg3 = showMessageBox;

            myCarsCopy.Sort(new Comparison<Car>(arg1));
            myCarsCopy.Reverse();
            foreach (var car in myCarsCopy)
            {
                Console.WriteLine(car);
            }

            myCarsCopy.FindAll(arg2).ForEach(arg3);

        }

    }
}
