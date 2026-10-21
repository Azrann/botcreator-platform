using System;
using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public static class StringExtensions
    {
        public static string ReplaceParameters(this string value, Dictionary<string, object> parameters)
        {
            try
            {
                foreach (var parametersKey in parameters.Keys)
                {
                    var replaceValue = string.Empty;
                    if (parameters[parametersKey] != null)
                    {
                        replaceValue = parameters[parametersKey] is decimal ? ((decimal) parameters[parametersKey]).ToString("G29") : parameters[parametersKey].ToString();
                    }

                    value = value.Replace($"[{parametersKey}]", replaceValue);
                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}