using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain
{
    public class Producer : Person
    {
        public Producer(int id, string Name, DateTime dateOfBirth)
            : base(id, Name, dateOfBirth) { }

        public Producer() { }
    }
}
