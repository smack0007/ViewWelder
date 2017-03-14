using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewWelder
{
    public static class CollectionHelper
    {
        public static Type ExtractCollectionItemType(Type collectionType)
        {
            if (collectionType == null)
                throw new ArgumentNullException(nameof(collectionType));

            if (collectionType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)))
            {
                return collectionType.GetGenericArguments().First();
            }

            return null;            
        }
    }
}
