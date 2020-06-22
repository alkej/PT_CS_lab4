using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace PT_lab4
{
    public class Engine : IComparable
    {
        private double displacement;
        private double horsePower;
        private string model;

        public double Displacement { get => displacement; set => displacement = value; }
        public double HorsePower { get => horsePower; set => horsePower = value; }
        public string Model { get => model; set => model = value; }

        public Engine() { }
        public Engine(double displacement, double hp, string model)
        {
            this.displacement = displacement;
            this.horsePower = hp;
            this.model = model;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Engine otherEngine = obj as Engine;
            if (otherEngine != null)
                return this.horsePower.CompareTo(otherEngine.horsePower);
            else
                throw new ArgumentException("Object is not a Engine");
        }

        public override string ToString()
        {
            return "" + this.model + " " + this.displacement + " (" + this.horsePower + " hp)";
        }
    }
}
