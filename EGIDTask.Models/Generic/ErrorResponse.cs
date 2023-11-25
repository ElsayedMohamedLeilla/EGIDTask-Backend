using EGIDTask.Enums;

namespace EGIDTask.Models.Generic
{
    public class ErrorResponse
    {
        public ResponseStatus State { get; set; }
        public string Message { get; set; }

    }
}
