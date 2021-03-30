using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain
{
    public class Enumerations
    {
        public enum OrderingBasis: byte
        {
            AlphabeticalAsc = 1,
            AlphabeticalDesc,
            DateAddedAsc,
            DateAddedDesc
        }
    }
}
