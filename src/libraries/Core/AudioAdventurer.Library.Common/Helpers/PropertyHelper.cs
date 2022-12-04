using System.Collections.Generic;
using System.Globalization;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class PropertyHelper
    {
        public static void SetSerializedJsonObjects<T>(
            this Dictionary<string, string> properties,
            string propertyName,
            List<T> values)
            where T : new()
        {
            properties[propertyName] = JsonHelper.Serialize(values);
        }

        public static List<T> GetSerializedJsonObjects<T>(
            this Dictionary<string, string> properties,
            string propertyName)
            where T: new()
        {
            if (properties.ContainsKey(propertyName))
            {
                string json = properties[propertyName];
                var temp = JsonHelper.Deserialize<List<T>>(json);
                return temp;
            }

            return new List<T>();
        }

        public static void SetSerializedJsonObject<T>(
            this Dictionary<string, string> properties,
            string propertyName,
            T value)
            where T : new()
        {
            properties[propertyName] = JsonHelper.Serialize(value);
        }

        public static T GetSerializedJsonObject<T>(
            this Dictionary<string, string> properties,
            string propertyName)
            where T : new()
        {
            if (properties.ContainsKey(propertyName))
            {
                string json = properties[propertyName];
                var temp = JsonHelper.Deserialize<T>(json);
                return temp;
            }

            return default(T);
        }

        public static string GetStringValue(
            this Dictionary<string, string> properties,
            string propertyName,
            string defaultValue = null)
        {
            if (properties.ContainsKey(propertyName))
            {
                string value = properties[propertyName];
                return value;
            }

            return defaultValue;
        }

        public static void SetValue(
            this Dictionary<string, string> properties,
            string propertyName,
            string value)
        {
            properties[propertyName] = value;
        }

        public static int GetIntValue(
            this Dictionary<string, string> properties,
            string propertyName,
            int defaultValue = 0)
        {
            if (properties.ContainsKey(propertyName))
            {
                string value = properties[propertyName];

                if (int.TryParse(value, out int result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public static void SetValue(
            this Dictionary<string, string> properties,
            string propertyName,
            int value)
        {
            properties[propertyName] = value.ToString();
        }


        public static double GetDoubleValue(
            this Dictionary<string, string> properties,
            string propertyName,
            double defaultValue = 0.0)
        {
            if (properties.ContainsKey(propertyName))
            {
                string value = properties[propertyName];

                if (double.TryParse(value, out double result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public static void SetValue(
            this Dictionary<string, string> properties,
            string propertyName,
            double value)
        {
            properties[propertyName] = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
