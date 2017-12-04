using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Validator<T> : IValidator<T>, IDataErrorInfo where T : IValidatable
    {
        private IDictionary<string, string> _errors = new Dictionary<string, string>();

        public IDictionary<string, string> Errors { get { return _errors; } }
        /// <summary>
        /// Returns if the object <typeparam>T</typeparam> has errors or not 
        /// </summary>
        public bool IsValid { get { return _errors.Count == 0; } }

        /// <summary>
        /// An abstract method that must be implemented 
        /// </summary>
        /// <param name="item"></param>
        public abstract void Validate(T item);

        /// <summary>
        /// Add the error to the dictionary if not exists already
        /// </summary>
        /// <param name="key">The Key</param>
        /// <param name="message">A composite format string.</param>
        /// <param name="parameters">An object array that contains zero or more objects to format.</param>
        public void AddError(string key, string message, params object[] parameters)
        {
            if (!_errors.ContainsKey(key))
                _errors.Add(key, string.Format(message, parameters));
        }
        /// <summary>
        /// Removes the message related to <paramref name="key"/> from the dictionary
        /// </summary>
        /// <param name="key">The key to remove from the dictionary</param>
        public void RemoveError(string key)
        {
            if (_errors.ContainsKey(key))
                _errors.Remove(key);
        }
        /// <summary>
        /// Clear all the messages stored in the dictionary
        /// </summary>
        public void Clear()
        {
            _errors.Clear();
        }
        /// <summary>
        /// Reports the message related to the <paramref name="key"/> from the dictionary
        /// </summary>
        /// <param name="key">The key of the message in the dictionary</param>
        /// <returns></returns>
        public string this[string key] { get { return _errors.ContainsKey(key) ? _errors[key] : string.Empty; } }

        string IDataErrorInfo.Error { get { return IsValid ? string.Empty : "Please correct the following errors and try again:"; } }

    }
}