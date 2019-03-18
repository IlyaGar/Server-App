using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class Ship
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public Ship(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
