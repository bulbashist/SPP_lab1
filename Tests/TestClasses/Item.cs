using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class Item
    {

        public string name;
        public int price;
        private float chance;

        public Item(string name, float chance)
        {
            this.name = name;
            this.chance = chance;
        }

        public override string ToString()
        {
            return $"Item: \nName: {this.name} \nPrice: {this.price} \nChance: {this.chance} \n";
        }
    }
}
