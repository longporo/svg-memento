using System;

namespace svg_memento.exception
{
    public class MyException : ApplicationException
    {
        public string error;
        private Exception innerException;

        public MyException() { }
        public MyException(string msg) : base(msg)
        {
            this.error = msg;
        }
        public MyException(string msg, Exception innerException) : base(msg, innerException)
        {
            this.innerException = innerException;
            error = msg;
        }
        public string GetError()
        {
            return error;
        }
    }
}