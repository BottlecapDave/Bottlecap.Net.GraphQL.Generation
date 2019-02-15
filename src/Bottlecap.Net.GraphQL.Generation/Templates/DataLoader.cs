using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class DataLoader : BaseTemplate
    {
        public string ReturnType { get; private set; }

        public string Name { get; private set; }

        public string ClassName { get; private set; }

        public string KeyType { get; private set; }

        public string PrimaryKeyName { get; private set; }

        public List<string> AdditionalParameters { get; private set; } = new List<string>();

        public List<string> AdditionalParameterNames { get; private set; } = new List<string>();

        public DataLoader(MethodInfo method, Type keyType, Type returnType)
        {
            ReturnType = returnType.GetCSharpTypeName();
            Name = method.Name;
            ClassName = method.DeclaringType.GetCSharpTypeName();
            KeyType = keyType.GetCSharpTypeName();
            
            var parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (i == 0)
                {
                    PrimaryKeyName = parameter.Name;
                }
                else
                {
                    AdditionalParameters.Add($"{parameter.ParameterType.GetCSharpTypeName()} {parameter.Name}");
                    AdditionalParameterNames.Add(parameters[i].Name);
                }
            }
        }
    }
}