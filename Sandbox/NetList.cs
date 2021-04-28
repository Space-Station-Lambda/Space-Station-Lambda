using System.Data;

namespace Sandbox
{
    public interface NetList<T>
    {
        void Clear();
        void Get(Type type);
        void Set(Type type, T value);
    }
}