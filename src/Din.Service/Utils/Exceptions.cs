using System;

namespace Din.Service.Utils
{ 
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message)
        {
        }
    }
    
    public class LoginException : Exception
    {
        public int Identifier { get; set; }
        public LoginException(string message, int id) : base(message)
        {
            Identifier = id;
        }
    }
}