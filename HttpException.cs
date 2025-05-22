using System.Net;

namespace MyBookApi;

public class HttpException : Exception
{
    public int StatusCode { get; }

    public HttpException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = (int)statusCode;
    }
}

public class NotFoundException : HttpException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound) { }
}

public class BadRequestException : HttpException
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest) { }
}

public class UnauthorizedException : HttpException
{
    public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized) { }
}

public class ForbiddenException : HttpException
{
    public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden) { }
}