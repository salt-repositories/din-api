using System;

namespace Din.Tests
{
    public class TestConsts
    {
        //User Properties
        public const string Firstname = "John";
        public const string Lastname = "Doe";
       
        //Account properties
        public const string Id = "1";
        public const string Role = "User";
        public const string Username = "Username";
        public const string Password = "HarDtoGuessPassword";
        public const string ImageName = "ImageName";

        //Session properties
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:61.0) Gecko/20100101 Firefox/61.0";
        public const string PublicIp = "0.0.0.0";
        public const string ExceptionMessage = "Incorrect username";
        public const int ExceptionId = 1;
        public const int LoginFailStatusCode = 400;
    }
}
