using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject;
using ServerApp.Models;
using ServerApp.Repositories;

namespace ServerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Bind<IRepository>().To<Repository>();
            kernel.Bind<Repository>().ToSelf();

            //List<Ship> listShips = new List<Ship>() { new Ship { Name = "Enterprise", Path = @"c:\1" }, new Ship { Name = "Normandy", Path = @"c:\2" } };
            if (!File.Exists(@"Content\ships.json"))
            {
                List<Ship> listShips = new List<Ship>() { new Ship("Enterprise", @"Content\USS_1.jpg"), new Ship("Normandy", @"Content\normandy-sr-2.jpg") };
                kernel.Get<IRepository>().CreateFile(listShips);
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
