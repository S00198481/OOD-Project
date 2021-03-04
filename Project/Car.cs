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

        public List<Modification> Mods { get; set; }

        public string ImageUrl { get; set; }

        //ctors
        public Car(){}

        public Car(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url)
        {
            Name = name;
            TopSpeed = topSpeed;
            ZeroTo100 = zeroTo100;
            Horsepower = horsePower;
            Torque = torque;
            MaxRpm = maxRpm;
            FuelMpg = mpg;
            Mods = new List<Modification>();
            ImageUrl = url;
        }

        //methods
        public override string ToString()
        {
            return this.Name;
        }
    }

    class Coupe : Car
    {
        //ctor
        public Coupe(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url)
        {}
    }

    class Hatchback : Car
    {
        //ctor
        public Hatchback(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url)
        { }
    }

    class Saloon : Car
    {
        //ctor
        public Saloon(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url)
        { }
    }

    class Estate : Car
    {
        //ctor
        public Estate(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url)
        { }
    }

    class Modded : Car
    {
        //ctor
        public Modded(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url)
        { }
    }
}
