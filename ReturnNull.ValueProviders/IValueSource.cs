using System.Collections.Generic;

namespace ReturnNull.ValueProviders
{
    public interface IValueSource
    {
        IEnumerable<T> GetValues<T>(string key);
    }
}
