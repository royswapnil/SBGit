using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SterlingBankLMS.Web.Utility
{
    public static class ContainerExtensions
    {
        public static void RegisterClosedTypesOf(this ContainerBuilder builder, Type openGenericServiceType, Assembly assembly)
        {
            if (openGenericServiceType == null) throw new ArgumentNullException("openGenericServiceType");
            if (!(openGenericServiceType.IsGenericTypeDefinition || openGenericServiceType.ContainsGenericParameters))
            {
                throw new ArgumentException(
                    string.Format("The type '{0}' is not an open generic class or interface type.",
                                  openGenericServiceType.FullName));
            }

            foreach (Type candidateType in assembly.GetTypes())
            {
                Type closedServiceType;
                if (findAssignableTypeThatCloses(candidateType, openGenericServiceType, out closedServiceType))
                {
                    builder.RegisterType(candidateType).As(closedServiceType).InstancePerRequest();
                }
            }
        }
        private static bool findAssignableTypeThatCloses(Type candidateType, Type openGenericServiceType, out Type closedServiceType)
        {
            closedServiceType = null;

            if (candidateType.IsAbstract) return false;

            foreach (Type interfaceType in getTypesAssignableFrom(candidateType))
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGenericServiceType)
                {
                    closedServiceType = interfaceType;
                    return true;
                }
            }

            return false;
        }
        private static IEnumerable<Type> getTypesAssignableFrom(Type candidateType)
        {
            foreach (Type interfaceType in candidateType.GetInterfaces())
            {
                yield return interfaceType;
            }

            Type nextType = candidateType;
            while (nextType != typeof(object))
            {
                yield return nextType;
                nextType = nextType.BaseType;
            }
        }
    }
}