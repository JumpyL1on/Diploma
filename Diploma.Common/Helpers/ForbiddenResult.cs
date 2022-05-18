using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class ForbiddenResult<T> : Result<T>
{
    public ForbiddenResult(List<string> errors) : base(ResultType.Forbidden, errors)
    {
    }

    public ForbiddenResult(string error) : this(new List<string> { error })
    {
    }
}