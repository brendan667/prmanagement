using System;
using System.Collections.Generic;
using System.Linq;

namespace LP.PRManagement.Common
{
    public static class ExtensionHelper
    {
        public static string StringJoin(this IEnumerable<object> values, string separator = ", ")
        {
            if (values == null) return null;
            var array = values.Select(x => x.ToString()).ToArray();
            return string.Join(separator, array);
        }

        public static Exception ToFirstExceptionOfException(this Exception exception)
        {
            var aggregateException = exception as AggregateException;
            if (aggregateException != null) return aggregateException.ToSimpleException();
            return exception;
        }

        public static Exception ToSimpleException(this AggregateException exception)
        {
            if (exception.InnerExceptions.Count == 1)
            {
                return exception.InnerExceptions.First();
            }
            return new Exception(exception.InnerExceptions.Select(x => x.Message).StringJoin(), exception);
        }
    }
}
