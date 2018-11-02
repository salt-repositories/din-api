using System;
using System.Collections.Generic;
using System.Text;

namespace Din.Service.Dto.Auth
{
    public class AuthResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
