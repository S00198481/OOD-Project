using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class ModificationClass
    {
        //attributes
        public string Name { get; set; }

        public int TopSpeedMod { get; set; }

        public int HorsepowerMod { get; set; }

        public double ZeroTo100Mod { get; set; }

        public int Index { get; set; }

        public string SetupName { get; set; }

        //constructors
        public ModificationClass() { }

        public ModificationClass(string name, int speed, int horsepower, double acceleration, int index)
        {
            Name = name;
            TopSpeedMod = speed;
            HorsepowerMod = horsepower;
            ZeroTo100Mod = acceleration;
            Index = index;
        }

        public ModificationClass(string name, string setupName)
        {
            Name = name;
            SetupName = setupName;
        }

    }
}
