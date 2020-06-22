using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace PT_lab4
{
    public class Car
    {
        private string model;
        private Engine motor;
        private int year;

        public string Model { get => model; set => model = value; }
        public Engine Motor { get => motor; set => motor = value; }
        public int Year { get => year; set => year = value; }

        public Car() {
            Motor = new Engine();
        }
        public Car(string model, Engine motor, int year) {

            this.model = model;
            this.motor = motor;
            this.year = year;
            
        }

        public override string ToString()
        {
            return "Samochód: " + this.model + " " + this.year + " Silnik: " + this.Motor;
        }
    }
}
