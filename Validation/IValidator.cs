using System.Collections.Generic;

namespace Validation
{
    public interface IValidator<T> where T : IValidatable
    {
        IDictionary<string, string> Errors { get; }
        bool IsValid { get; }

        void Validate(T item);
    }
}