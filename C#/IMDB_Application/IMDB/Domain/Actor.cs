using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain
{
    public class Actor : Person
    {
        public Actor(int id, string name, DateTime dateOfBrith)
            : base(id, name, dateOfBrith)
        { }

        public Actor() { }
    }
}
