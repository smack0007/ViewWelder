using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ViewWelder.ViewModels;

namespace ViewWelder
{
    internal static class TypeExtensions
    {
        public static Type GetCollectionElementType(this Type type)
        {
            if (IsCollection(type))
            {
                return type.GetGenericArguments().First();
            }

            return null;
        }

        public static bool IsCollection(this Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public static bool IsViewModel(this Type type)
        {
            return typeof(ViewModelBase).IsAssignableFrom(type);
        }
    }
}
