using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Helpers
{
    public sealed class ConstantWebAPI
    {
        private ConstantWebAPI() { }

        public const int IntClientTimeSpan = 100;
        public const int IntServerTimeSpan = 100;

        public const int CheapGatewayRetries = 1;
        public const int PremiumPaymentRetries = 3;
    }
}
