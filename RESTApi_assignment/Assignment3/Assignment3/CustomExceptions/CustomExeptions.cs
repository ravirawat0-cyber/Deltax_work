using System;

namespace Assignment3.CustomExceptions
{
    public class SqlException : Exception
    {
        public SqlException(string message) : base (message) { }
    }
}
