using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Repositories
{
    public interface IRepository
    {
        void CreateFile(List<Ship> ship);

        List<Ship> ReadFile();

        Ship Create(string name, byte[] arr);

        Ship GetShip(string name);

        void Update(string name, byte[] arr);

        void Delete(string name);
    }
}
