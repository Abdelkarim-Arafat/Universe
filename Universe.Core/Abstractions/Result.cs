using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Abstractions;

public class Result
{
    public Result(bool isSuccess , Error error)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; private init; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; private init; } = null!;

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value , true , Error.None);
    public static Result<T> Failure<T>(Error error) => new(default! , false , error);
}

public class Result<T> : Result
{
    public Result(T value , bool isSuccess , Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }
    public T Value
    {
        get => IsSuccess
            ? field
            : throw new InvalidOperationException("Failure results cannot have value");
        private init;
    }
}