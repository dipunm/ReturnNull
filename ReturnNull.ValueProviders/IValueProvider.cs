using System.Collections.Generic;

namespace ReturnNull.ValueProviders
{
    public interface IValueProvider
    {
        ValueProvider Excluding(params string[] sources);
        T GetValue<T>(string key, T defaultValue = default(T));
        IEnumerable<T> GetValues<T>(string key);
        ValueProvider LimitedTo(params string[] sources);
        ValueProvider Preferring(params string[] sources);
        bool TryGetValue<T>(string key, out T value);
    }
}