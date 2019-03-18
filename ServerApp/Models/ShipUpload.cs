using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class ShipUpload
    {
        public string Name { get; set; }

        public byte[] ImgByte { get; set; }

        public ShipUpload(string name, byte[] img)
        {
            Name = name;
            ImgByte = img;
        }
    }
}
