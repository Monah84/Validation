using System;
using System.Linq;
using System.Reflection;

namespace Validation
{
    public static class ValidationFactory
    {
        public static Validator<T> Validate<T>(this T item) where T : IValidatable
        {
            var type = typeof(Validator<T>);
            var assembly = Assembly.GetAssembly(typeof(T));

            var validator = assembly.GetTypes()
                               .Where(t => !t.IsAbstract && (type.IsAssignableFrom(t) ||
                                      (type.IsGenericType && type.GetGenericTypeDefinition() == t)))
                               .FirstOrDefault();
            if (validator != null)
            {
                var method = validator.GetMethods()
                         .Where(t => t.Name == "Validate")
                         .FirstOrDefault();
                if (method != null)
                {
                    var obj = Activator.CreateInstance(validator);
                    method.Invoke(obj, new object[] { item });
                    //var details = obj.GetType().GetProperty("Errors").GetValue(obj, null);
                    //return details as Dictionary<string, string>;
                    return obj as Validator<T>;
                }
            }
            return null;
        }
    }
}