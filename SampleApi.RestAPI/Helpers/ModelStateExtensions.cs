using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleApi.RestAPI.Models;
using System.Linq;

namespace SampleApi.RestAPI.Helpers
{
    public static class ModelStateExtensions {

        public static ApiErrorResponse ToApiErrorResponse(this ModelStateDictionary keyValues, int errorCode) {

            var errorsInModelState = keyValues.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            var errorResponse = new ApiErrorResponse();

            foreach(var error in errorsInModelState) {
                foreach(var subError in error.Value) {
                    var apiError = new ApiError {
                        Code = errorCode,
                        Message = subError
                    };
                    errorResponse.Errors.Add(apiError);
                }
            }

            return errorResponse;
        }

    }
}
