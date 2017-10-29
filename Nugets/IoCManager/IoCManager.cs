using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoCManager
{
    public static class IoCManager
    {
        public static T GetSinglenstance<T>(string dllStartName)
        {
            var builder = new ContainerBuilder();

            Assembly[] assemblies = GetAssembliesFromApplicationBaseDirectory(x => x.FullName.StartsWith(dllStartName));
            Type typeParameterType = typeof(T);
            string typeName = typeParameterType.Name;
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name == typeName).AsImplementedInterfaces();

            builder.RegisterAssemblyModules(assemblies);

            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();z


            //var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces()
                .Any(i => i.IsAssignableFrom(typeof(T))))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //.Where(t => t.IsAssignableFrom(typeof(IPluginMainWindow)))
            //.As<IPluginMainWindow>();

            var container = builder.Build();
            var pluginClasses = container.Resolve<IEnumerable<T>>();
            try
            {
                var element = pluginClasses.Single();
                return element;
            }
            catch (Exception ex)
            {

                throw new Exception("Not single implementaiton");
            }
           
        }

        private static Assembly[] GetAssembliesFromApplicationBaseDirectory(Func<AssemblyName, bool> condition)
        {
            try
            {
                string baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

                Func<string, bool> isAssembly = file => string.Equals(System.IO.Path.GetExtension(file), ".dll", StringComparison.OrdinalIgnoreCase);

                var files = Directory.GetFiles(baseDirectoryPath);
                var assemby = files.Where(isAssembly);
                var conditionAssembly = assemby.Where(f => condition(new AssemblyName(System.IO.Path.GetFileName(f))));
                var result = conditionAssembly.Select(Assembly.LoadFrom).ToArray();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
