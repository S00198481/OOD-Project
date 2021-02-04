using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    abstract class Car
    {
        //attributes
        public string Name { get; set; }

        public int TopSpeed { get; set; }

        public double ZeroTo100 { get; set; }

        public int Horsepower { get; set; }

        public int Torque { get; set; }

        public int MaxRpm { get; set; }

        public int FuelMpg { get; set; }

        //ctors
        public Car(){}

        public Car(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg)
        {
            Name = name;
            TopSpeed = topSpeed;
            ZeroTo100 = zeroTo100;
            Horsepower = horsePower;
            Torque = torque;
            MaxRpm = maxRpm;
            FuelMpg = mpg;
        }
    }

    class Coupe : Car
    {
        //ctor
        public Coupe(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg)
        {}
    }

    class Hatchback : Car
    {
        //ctor
        public Hatchback(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg)
        { }
    }

    class Saloon : Car
    {
        //ctor
        public Saloon(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg)
        { }
    }

    class Estate : Car
    {
        //ctor
        public Estate(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg)
        { }
    }
}
