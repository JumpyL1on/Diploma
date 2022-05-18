using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class NotFoundResult<T> : Result<T>
{
    public NotFoundResult(List<string> errors) : base(ResultType.NotFound, errors)
    {
    }

    public NotFoundResult(string error) : this(new List<string> { error })
    {
    }
}