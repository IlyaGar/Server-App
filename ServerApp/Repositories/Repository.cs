using Newtonsoft.Json;
using ServerApp.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Repositories
{
    public class Repository : IRepository
    {
        private readonly string _folderName;
        private readonly string _fileName;

        public Repository()
        {
            _folderName = @"Content\";
            _fileName = _folderName + "ships.json";
        }

        public void CreateFile(List<Ship> listShips)
        {
            using (StreamWriter file = File.CreateText(_fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listShips);
            }
        }

        public List<Ship> ReadFile()
        {
            string json = File.ReadAllText(_fileName);
            var listShips = JsonConvert.DeserializeObject<List<Ship>>(json);
            for (int i = 0; i < listShips.Count(); i++)
                listShips[i].Path = new Uri(Path.GetFullPath(listShips[i].Path)).AbsoluteUri;
            return listShips;
        }

        public Ship Create(string name, byte[] arr)
        {
            var listShips = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(_fileName));
            Ship ship = new Ship(name, GetImageFromByteArray(arr));
            
            listShips.Add(ship);

            CreateFile(listShips);
            ship.Path = new Uri(Path.GetFullPath(ship.Path)).AbsoluteUri;
            return ship;
        }

        public Ship GetShip(string name)
        {
            var listShips = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(_fileName));

            return listShips.FirstOrDefault(s => s.Name == name);
        }


        public void Update(string name, byte[] arr)
        {
            var listShips = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(_fileName));
            listShips.Where(l => l.Name == name)
               .Select(l => { l.Path = GetImageFromByteArray(arr); return l; })
               .ToList();
            CreateFile(listShips);
        }

        public void Delete(string name)
        {
            var listShips = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(_fileName));

            listShips.RemoveAll(l => l.Name == name);

            CreateFile(listShips);
        }

        private string GetImageFromByteArray(byte[] bytes)
        {
            var image = Image.Load<Rgba32>(bytes);
            image.Mutate(x => x.Grayscale());
            string nameImage = _folderName + Guid.NewGuid().ToString() + ".jpg";
            image.Save(nameImage);
            return nameImage;
        }
    }
}
