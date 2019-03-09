using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Diary
{
    class ClassFactory : IClassFactory
    {
        Dictionary<Type, Type> classDict = new Dictionary<Type, Type>();

        public ClassFactory(XDocument XMLSettings)
        {
            this.LoadSettingsFromFile(XMLSettings);
        }

        private void LoadSettingsFromFile(XDocument XMLSettings)
        {
            XElement root = XMLSettings.Root;
            foreach (XElement settingsPair in root.Elements("element"))
            {
                var interfaceElement = settingsPair.Element("interface");
                var classElement = settingsPair.Element("class");

                var interfaceName = interfaceElement.Value;
                var className = classElement.Value;

                var executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;

                Type interfaceType = Type.GetType($"Diary.{interfaceName}, {executingAssemblyName}");
                Type classType = Type.GetType($"Diary.{className}, {executingAssemblyName}");
                if (interfaceType == null || classType == null)
                {
                    var invalidInterface = interfaceType == null;
                    var invalidClass = classType == null;
                    var message = "";
                    if (invalidInterface && invalidClass)
                    {
                        message = $"Invalid interface and class name: \"{interfaceName}\" and \"{className}\"";
                    } else if (invalidInterface && !invalidClass)
                    {
                        message = $"Invalid interface name: \"${interfaceName}\"";
                    } else if (!invalidInterface && invalidClass)
                    {
                        message = $"Invalid class name: \"{className}\"";
                    }
                    throw new InvalidDataException(message);
                }
                classDict.Add(interfaceType, classType);
            }
        }

        public T Create<T>(params object[] args) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new InvalidOperationException("Generic type T must be an interface.");
            }
            if (!classDict.ContainsKey(typeof(T)))
            {
                throw new MissingMemberException("Interface not found.", typeof(T).ToString());
            }

            Type objType = classDict[typeof(T)];
            T obj = (T)Activator.CreateInstance(objType, args);
            return obj;
        }

        public T Create<T>() where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new InvalidOperationException("Generic type T must be an interface.");
            }
            if (!classDict.ContainsKey(typeof(T)))
            {
                throw new MissingMemberException("Interface not found.", typeof(T).ToString());
            }

            Type objType = classDict[typeof(T)];
            T obj = (T)Activator.CreateInstance(objType);
            return obj;
        }
    }
}
