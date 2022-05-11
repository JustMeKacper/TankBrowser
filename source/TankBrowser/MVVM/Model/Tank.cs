using System;
using System.Collections.Generic;
namespace TankBrowser.MVVM.Model
{
    public class Tank
    {
        Random rnd = new Random();
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public Tank(string name, string desc, string ImageSource = "")
        {
            this.Name = name;
            this.Description = desc;
            while (true)
            {
                if (ImageSource.StartsWith(" "))
                    ImageSource.Remove(0);
                else
                    break;
            }
            this.ImageName = ImageSource;
        }
        
        
        private Tank GetTank(string name, string desc, string imageName)
        {
            Tank output = new Tank(name,desc,imageName);
            return output;
        }
        public string getTankName(Tank tank)
        {
            return tank.Name;
        }
        public string getTankDesc(Tank tank)
        {
            return tank.Description;
        }
        public string getTankImageName(Tank tank)
        {
            return tank.ImageName;
        }

    }
}
