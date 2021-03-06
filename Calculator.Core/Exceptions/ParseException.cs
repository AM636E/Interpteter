﻿using System;

namespace Calculator.Core.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(string text) : base(text)
        { }
    }

    public class UnexpectedTokenException : ParseException
    {
        public UnexpectedTokenException(TokenType type) : base($"Unexpected token: {type}") { }
    }
}
