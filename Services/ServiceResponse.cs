using System.Collections.Generic;

namespace Services
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}