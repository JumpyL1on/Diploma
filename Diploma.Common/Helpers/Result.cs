using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public abstract class Result<T>
{
    public Result(ResultType resultType, T data)
    {
        ResultType = resultType;
        Data = data;
        Errors = default;
    }

    public Result(ResultType resultType, List<string> errors)
    {
        ResultType = resultType;
        Data = default;
        Errors = errors;
    }

    public Result(ResultType resultType)
    {
        ResultType = resultType;
        Data = default;
        Errors = default;
    }

    public ResultType ResultType { get; }
    public T? Data { get; }
    public List<string>? Errors { get; }
}