﻿using System;

namespace Calculator.Core.Exceptions
{
    public class InterpertationException : Exception
    {
        public InterpertationException(string reason) : base(reason) { }
    }

    public class UnexpectedOperationException : InterpertationException
    {
        public UnexpectedOperationException(Token token) : base($"Unexpected token: [{token.Type}|{token.Value}]") { }
    }

    public class UnknownSymbolException : InterpertationException
    {
        public UnknownSymbolException(string symbolName) : base($"Unknown symbol: {symbolName}.") { }
    }

    public class ZeroDivizionException : InterpertationException
    {
        public ZeroDivizionException() : base("(Zero division)") { }
    }
}