using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spy.Entities
{

    public class Watch
    {
        public string Path { get; private set; }
        public string Extension { get; private set; }
        public string Classe { get; private set; }
        public string Method { get; private set; }
        public string AssemblyName { get; private set; }

        private event EventHandler OnCreated;
        private void On_Created(object sender, EventArgs e)
        {
            ExecuteMethod();
        }

        private void ExecuteMethod()
        {
            Assembly assembly = Assembly.LoadFrom($"{AssemblyName}.dll");
            var type = assembly.GetTypes().SingleOrDefault(x => x.Name == Classe);
            ConstructorInfo Constructor = type.GetConstructor(Type.EmptyTypes);
            object obj = Constructor.Invoke(new object[] { });
            MethodInfo methodInfo = type.GetMethod(Method);
            methodInfo.Invoke(obj, null);
        }

        public Watch(string path, string extension, string assemblyName, string classe, string method)
        {
            AssemblyName = assemblyName;
            Path = path;
            Extension = extension;
            Classe = classe;
            Method = method;
            OnCreated += On_Created;
            Towork();
        }


        private void Towork()
        {

            var watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.Filter = $"*.{Extension}";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;
        }

    }

}
