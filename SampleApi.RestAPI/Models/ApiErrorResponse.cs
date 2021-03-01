using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Models
{
    public class ApiErrorResponse {
        /// <summary>
        /// The list of customer errors.
        /// </summary>
        public List<ApiError> Errors { get; set; } = new List<ApiError>();
    }
}
