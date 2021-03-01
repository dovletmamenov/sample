using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Models
{
    ///<summary>
    ///| Code | Description                                                                                                     |
    ///| ---- | ------------------                                                                                              |
    ///| 100  | The request body can not be parsed. Ensure that request payload syntax is proper.                               |
    ///| 101  | Input validation error happened. Some of the fields have invalid data. See the error message for more details.  |
    ///| 200  | Client does not have permission to create Location.                                                             |
    ///| 201  | Duplicate locations are not allowed                                                                             |
    ///| 999  | Other error.                                                                                                    |
    ///</summary>
    public class ApiError {

        /// <summary>
        /// Machine readable error code
        /// </summary>
        /// <example>123</example>
        public int Code { get; set; }

        /// <summary>
        /// Human readable error message
        /// </summary>
        /// <example>Description of the error</example>
        public string Message { get; set; }
    }
}
