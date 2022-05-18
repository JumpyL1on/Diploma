using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class UnprocessableEntityResult<T> : Result<T>
{
    public UnprocessableEntityResult(List<string> errors) : base(ResultType.UnprocessableEntity, errors)
    {
    }

    public UnprocessableEntityResult(string error) : this(new List<string> { error })
    {
    }
}