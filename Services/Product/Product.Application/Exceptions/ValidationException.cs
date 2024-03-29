﻿using FluentValidation.Results;

namespace Product.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

    }

    public ValidationException(string name, object key, string message)
        : base($"Entity \"{name}\" ({key}) ({message})")
    {
    }

    public ValidationException(string message)
        : base(message)
    {
        Errors = new Dictionary<string, string[]>();
        Errors.Add(message, new string[] { });
    }
    public IDictionary<string, string[]> Errors { get; }
}