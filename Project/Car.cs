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
        public string Info { get; set; }

        //ctors
        public Car(){}

        public Car(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info)
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
            Info = info;
        }

        //methods
        public override string ToString()
        {
            return this.Name;
        }
    }

    class Coupe : Car
    {
        public string Icon { get; set; }
        //ctor
        public Coupe(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url, info)
        {
            Icon = "/images/coupe.png";
        }
    }

    class Hatchback : Car
    {
        public string Icon { get; set; }
        //ctor
        public Hatchback(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url, info)
        {
            Icon = "/images/hatchback.png";
        }
    }

    class Saloon : Car
    {
        public string Icon { get; set; }
        //ctor
        public Saloon(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url, info)
        {
            Icon = "/images/saloon.png";
        }
    }

    class Estate : Car
    {
        public string Icon { get; set; }
        //ctor
        public Estate(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url, info)
        {
            Icon = "/images/estate.png";
        }
    }

    class Modded : Car
    {
        public string Icon { get; set; }
        //ctor
        public Modded(string name, int topSpeed, double zeroTo100, int horsePower, int torque, int maxRpm, int mpg, string url, string info) : base(name, topSpeed, zeroTo100, horsePower, torque, maxRpm, mpg, url, info)
        {
            Icon = "/images/cog.png";
        }
    }
}
