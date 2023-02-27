using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Exeptions
{
    public sealed class InvalidDataExeption : Exception
    {
        public InvalidDataExeption(string name, int id)
            : base($"Entity {name} {id} is invalid ")
        {
        }

        public InvalidDataExeption(string name) : base($"Entity {name} is invalid.") { }
    }
}
