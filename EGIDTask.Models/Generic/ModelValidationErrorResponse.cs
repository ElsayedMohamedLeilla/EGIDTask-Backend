using EGIDTask.Enums;

namespace EGIDTask.Models.Generic
{
    public class ModelValidationErrorResponse
    {
        public ModelValidationErrorResponse()
        {
            Errors = new List<ErrorModel>();
        }
        public ResponseStatus State { get; set; }
        public string Message { get; set; }
        public List<ErrorModel> Errors { get; set; }

    }
}
