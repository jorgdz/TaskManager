using System;
using Newtonsoft.Json;
 
namespace TaskManager.CustomExceptions
{
    public class ErrorResponse
    {
        public DateTime TimeStamps { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}