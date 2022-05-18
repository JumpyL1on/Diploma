using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class NoContentResult<T> : Result<T>
{
    public NoContentResult() : base(ResultType.NoContent)
    {
    }
}