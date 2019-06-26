using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    public interface IValidator<T>
    {
        bool Validate(T t);
        IList<string> ValidationErrors { get; }
    }
}