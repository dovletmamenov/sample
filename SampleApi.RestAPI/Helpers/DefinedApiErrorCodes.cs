using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Helpers
{
    public static class DefinedApiErrorCodes
    {
        public static readonly int ProblemParsing = 100;

        public static readonly int InputValidation = 101;

        public static readonly int LocationcreationIsForbidden = 200;
    }
}
