﻿namespace StackOverflow.Services.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message)
        { }
    }
}
