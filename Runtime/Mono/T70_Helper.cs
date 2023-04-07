using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class T70_Helper
{
    public static object GetValue(string value, Type type)
    {
        if (type == typeof(string)) return value;


        if (string.IsNullOrEmpty(value))
        {
            Debug.LogWarning("Value cannot be empty");
            return null;
        }

        if (type == typeof(int))
        {
            int.TryParse(value, out int intV);
            return intV;
        }

        if (type == typeof(float))
        {
            float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float intV);
            return intV;
        }

        if (type == typeof(double))
        {
            double.TryParse(value, out double intV);
            return intV;
        }
        if (type.IsEnum)
        {
            try
            {
                return Enum.Parse(type, value);
            }
            catch
            {
                return null;
            }

        }
        if(type.IsClass)
        {
            try
            {
                return JsonUtility.FromJson(value, type);
            }
            catch
            {
                return null;
            }

        }
        Debug.LogWarning("Unsupported value with type: " + type);
        return null;

    }
}
