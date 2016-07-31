using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnNull.ValueProviders
{
    public class ValueProvider
    {
        private readonly IDictionary<string, IValueSource> _datasources;

        public ValueProvider(IDictionary<string, IValueSource> datasources)
        {
            if (datasources == null) throw new ArgumentNullException(nameof(datasources));
            if (!datasources.Any()) throw new ArgumentException("Cannot create a value provider with no datasources.", nameof(datasources));
            _datasources = datasources;
        }

        public IEnumerable<T> GetValues<T>(string key)
        {
            return _datasources.SelectMany(s => s.Value.GetValues<T>(key));
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            var values = GetValues<T>("key").Take(1).ToList();
            return values.Any() ? values.First() : defaultValue;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var values = GetValues<T>("key").Take(1).ToList();
            if (values.Any())
            {
                value = values.First();
                return true;
            }
            value = default(T);
            return false;
        }

        public ValueProvider Excluding(params string[] sources)
        {
            var newSources = new Dictionary<string, IValueSource>(_datasources);
            foreach (var key in sources)
            {
                newSources.Remove(key);
            }

            return new ValueProvider(newSources);
        }

        public ValueProvider LimitedTo(params string[] sources)
        {
            ThrowIfMissing(sources);

            var exclude = _datasources.Keys.Except(sources);
            return Excluding(exclude.ToArray());
        }

        public ValueProvider Preferring(params string[] sources)
        {
            ThrowIfMissing(sources);

            var preferred = sources.ToDictionary(s => s, s => _datasources[s]);
            var others = _datasources.Keys.Except(sources).ToDictionary(s => s, s => _datasources[s]);
            return new ValueProvider(
                preferred
                    .Concat(others)
                    .ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        private void ThrowIfMissing(string[] sources)
        {
            var missingSources = sources.Except(_datasources.Keys).ToList();
            if (missingSources.Any())
                throw new ArgumentException($"Cannot find sources with names: '{string.Join("', '", missingSources)}'", nameof(sources));
        }
    }
}