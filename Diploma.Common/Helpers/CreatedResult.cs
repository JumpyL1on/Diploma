using Diploma.Common.Enums;

namespace Diploma.Common.Helpers;

public class CreatedResult<T> : Result<T>
{
    public CreatedResult() : base(ResultType.Created)
    {
    }
}