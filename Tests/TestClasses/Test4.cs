using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class Test4
    {
        int a;
        public int b { get; private set; }
        public Test4(int a, int b)
        {
            this.b = 2;
        }

        public Test4()
        {
            this.b = 0;
        }
    }
}
