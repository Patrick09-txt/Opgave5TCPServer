using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.model
{
    public class FootballPlayer
    {
        public int _id;
        private string _name;
        private int _price;
        private int _shirtNumber;

        public FootballPlayer()
        {

        }

        public FootballPlayer(string name, int price, int shirtNumber)
        {
            Name = name;
            Price = price;
            ShirtNumber = shirtNumber;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length < 4) throw new ArgumentOutOfRangeException("Name", Name, "Name must be 4 characters or longer");
                _name = value;
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Price", Price, "Price must be above 0");
                _price = value;
            }
        }

        public int ShirtNumber
        {
            get { return _shirtNumber; }
            set
            {
                if (value < 1 || value > 100) throw new ArgumentOutOfRangeException("Shirt Number", ShirtNumber, "Shirt Number must be between 1 and 100");
                _shirtNumber = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(ShirtNumber)}: {ShirtNumber}";
        }
    }
}
