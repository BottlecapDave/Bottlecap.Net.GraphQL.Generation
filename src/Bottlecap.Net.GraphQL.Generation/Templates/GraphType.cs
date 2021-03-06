﻿using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class GraphType : BaseTemplate
    {
        private readonly TypeDefinition _typeDefinition;

        public bool IsInput
        {
            get
            {
                return _typeDefinition.IsInput;
            }
        }

        public bool IsOutput { get { return _typeDefinition.IsInput == false; } }

        public string GraphClassName
        {
            get
            {
                if (IsInput)
                {
                    return $"{Name}InputGraphType";
                }
                else
                {
                    return $"{Name}GraphType";
                }
            }
        }

        public string ClassName
        {
            get
            {
                return _typeDefinition.Type.Name;
            }
        }

        public string Name
        {
            get
            {
                return _typeDefinition.Name;
            }
        }

        public string Namespace
        {
            get
            {
                return _typeDefinition.Type.Namespace;
            }
        }

        public List<BaseFieldType> Fields { get; private set; }

        public GraphType(Generator generator, TypeDefinition type)
            :base (generator)
        {
            _typeDefinition = type;
            Fields = new List<BaseFieldType>();

            foreach (var propertyDefinition in _typeDefinition.PropertyDefinitions)
            {
                if (propertyDefinition.Overrides.IsIgnored)
                {
                    continue;
                }

                if (((propertyDefinition.Property.PropertyType.IsValueType == false) && propertyDefinition.Property.PropertyType != typeof(string)) ||
                    propertyDefinition.Property.PropertyType.IsEnum)
                {
                    Fields.Add(new ExplicitFieldType(_generator, propertyDefinition));
                }
                else
                {
                    Fields.Add(new BaseFieldType(_generator, propertyDefinition));
                }
            }
        }
    }
}
