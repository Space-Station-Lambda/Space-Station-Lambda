using System;
using Sandbox;
using ssl.Modules.Elements;

namespace ssl
{
    public abstract class InstanceFactory<T1, T2> where T1 : BaseData
    {
        protected abstract string BasePath { get; }

        public abstract T2 Create(string prefix, string name);

        public T2 Create(T1 t1)
        {
            return Create(t1.Id);
        }

        public T2 Create(string id)
        {
            string prefix = id.Split(".")[0];
            string name = id.Split(".")[1];
            return Create(prefix, name);
        }

        protected string GetFilePath(string prefix, string name)
        {
            return $"{BasePath}/{prefix}/{name}.{prefix}";
        }

        protected T TryLoad<T>(string filePath) where T : Asset
        {
            T resourceToLoad = Resource.FromPath<T>(filePath);
            if (null == resourceToLoad) throw new ArgumentException("Invalid path file");
            return resourceToLoad;
        }
    }
}