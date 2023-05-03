using System;

namespace IMDB.CustomExceptions
{
    public class SqlException : Exception
    {
        public SqlException(string message) : base (message) { }
    }
}
