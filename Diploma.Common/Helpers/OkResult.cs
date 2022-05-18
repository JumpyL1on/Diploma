using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class OkResult<T> : Result<T>
{
    public OkResult(T data) : base(ResultType.Ok, data)
    {
    }
}