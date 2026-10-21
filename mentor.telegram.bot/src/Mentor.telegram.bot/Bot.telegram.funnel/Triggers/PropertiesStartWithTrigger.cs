using System;
using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{
    public class PropertiesStartWithTrigger : ITrigger
    {
        public int Priority { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public PropertiesStartWithTrigger(Dictionary<string, object> properties)
        {
            Properties = properties;
        }

        public bool Check(Data data)
        {
            if (data.Properties == null && Properties.Count > 0)
            {
                return false;
            }

            foreach (var property in Properties)
            {
                if (data.Properties == null)
                {
                    return false;
                }

                if (!data.Properties.ContainsKey(property.Key))
                {
                    return false;
                }

                object value1 = data.Properties[property.Key];
                object value2 = property.Value;

                if (value1.IsNumericType() && value2.IsNumericType())
                {
                    if (Double.Parse(value1.ToString(), System.Globalization.NumberStyles.Float) !=
                        Double.Parse(value2.ToString(), System.Globalization.NumberStyles.Float))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!value1.ToString().StartsWith(value2.ToString()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}