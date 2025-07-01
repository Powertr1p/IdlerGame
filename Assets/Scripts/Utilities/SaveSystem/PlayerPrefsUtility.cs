using System;
using System.Reflection;
using UnityEngine;
using Utilities.SaveBox;

namespace SaveSystem
{
    public static class PlayerPrefsUtility
    {
        public static void SaveAll(object target)
        {
            if (target == null) return;
            
            Type type = target.GetType();
            
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = field.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null)
                {
                    SaveField(target, field, attribute);
                }
            }
            
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = property.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null && property.CanRead)
                {
                    SaveProperty(target, property, attribute);
                }
            }
            
            PlayerPrefs.Save();
        }
        
        public static void LoadAll(object target)
        {
            if (target == null) return;
            
            Type type = target.GetType();
            
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = field.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null)
                {
                    LoadField(target, field, attribute);
                }
            }
            
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = property.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null && property.CanWrite)
                {
                    LoadProperty(target, property, attribute);
                }
            }
        }
        
        public static void DeleteAll(object target)
        {
            if (target == null) return;
            
            Type type = target.GetType();
            
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = field.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null)
                {
                    string key = attribute.GetFullKey(field.Name);
                    PlayerPrefs.DeleteKey(key);
                }
            }
            
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = property.GetCustomAttribute<PlayerPrefsAttribute>();
                if (attribute != null)
                {
                    string key = attribute.GetFullKey(property.Name);
                    PlayerPrefs.DeleteKey(key);
                }
            }
            
            PlayerPrefs.Save();
        }
        
        private static void SaveField(object target, FieldInfo field, PlayerPrefsAttribute attribute)
        {
            string key = attribute.GetFullKey(field.Name);
            object value = field.GetValue(target);
            
            SaveValue(key, value, field.FieldType);
        }
        
        private static void SaveProperty(object target, PropertyInfo property, PlayerPrefsAttribute attribute)
        {
            string key = attribute.GetFullKey(property.Name);
            object value = property.GetValue(target);
            
            SaveValue(key, value, property.PropertyType);
        }
        
        private static void LoadField(object target, FieldInfo field, PlayerPrefsAttribute attribute)
        {
            string key = attribute.GetFullKey(field.Name);
            object value = LoadValue(key, field.FieldType, attribute);
            
            field.SetValue(target, value);
        }
        
        private static void LoadProperty(object target, PropertyInfo property, PlayerPrefsAttribute attribute)
        {
            string key = attribute.GetFullKey(property.Name);
            object value = LoadValue(key, property.PropertyType, attribute);
            
            property.SetValue(target, value);
        }
        
        private static void SaveValue(string key, object value, Type type)
        {
            if (value == null) return;
            
            if (type == typeof(int) || type == typeof(int?))
            {
                PlayerPrefs.SetInt(key, (int)value);
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                PlayerPrefs.SetFloat(key, (float)value);
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
            }
            else if (type == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)value);
            }
            else if (type == typeof(Vector2))
            {
                Vector2 vector = (Vector2)value;
                PlayerPrefs.SetFloat(key + ".x", vector.x);
                PlayerPrefs.SetFloat(key + ".y", vector.y);
            }
            else if (type == typeof(Vector3))
            {
                Vector3 vector = (Vector3)value;
                PlayerPrefs.SetFloat(key + ".x", vector.x);
                PlayerPrefs.SetFloat(key + ".y", vector.y);
                PlayerPrefs.SetFloat(key + ".z", vector.z);
            }
            else if (type == typeof(Color))
            {
                Color color = (Color)value;
                PlayerPrefs.SetFloat(key + ".r", color.r);
                PlayerPrefs.SetFloat(key + ".g", color.g);
                PlayerPrefs.SetFloat(key + ".b", color.b);
                PlayerPrefs.SetFloat(key + ".a", color.a);
            }
            else if (type.IsEnum)
            {
                PlayerPrefs.SetInt(key, Convert.ToInt32(value));
            }
            else if (type == typeof(DateTime))
            {
                PlayerPrefs.SetString(key, ((DateTime)value).ToBinary().ToString());
            }
            else
            {
                string json = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(key, json);
            }
        }
        
        private static object LoadValue(string key, Type type, PlayerPrefsAttribute attribute)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return GetDefaultValue(type, attribute);
            }
            
            if (type == typeof(int) || type == typeof(int?))
            {
                return PlayerPrefs.GetInt(key, (int)attribute.DefaultValue);
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return PlayerPrefs.GetFloat(key, attribute.DefaultValue);
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return PlayerPrefs.GetInt(key, attribute.DefaultBool ? 1 : 0) == 1;
            }
            else if (type == typeof(string))
            {
                return PlayerPrefs.GetString(key, attribute.DefaultString);
            }
            else if (type == typeof(Vector2))
            {
                Vector2 vector = new Vector2();
                vector.x = PlayerPrefs.GetFloat(key + ".x", 0f);
                vector.y = PlayerPrefs.GetFloat(key + ".y", 0f);
                return vector;
            }
            else if (type == typeof(Vector3))
            {
                Vector3 vector = new Vector3();
                vector.x = PlayerPrefs.GetFloat(key + ".x", 0f);
                vector.y = PlayerPrefs.GetFloat(key + ".y", 0f);
                vector.z = PlayerPrefs.GetFloat(key + ".z", 0f);
                return vector;
            }
            else if (type == typeof(Color))
            {
                Color color = new Color();
                color.r = PlayerPrefs.GetFloat(key + ".r", 0f);
                color.g = PlayerPrefs.GetFloat(key + ".g", 0f);
                color.b = PlayerPrefs.GetFloat(key + ".b", 0f);
                color.a = PlayerPrefs.GetFloat(key + ".a", 1f);
                return color;
            }
            else if (type.IsEnum)
            {
                int intValue = PlayerPrefs.GetInt(key, 0);
                return Enum.ToObject(type, intValue);
            }
            else if (type == typeof(DateTime))
            {
                string binaryString = PlayerPrefs.GetString(key, "0");
                long binary = 0;
                if (long.TryParse(binaryString, out binary))
                {
                    return DateTime.FromBinary(binary);
                }
                return DateTime.MinValue;
            }
            else
            {
                string json = PlayerPrefs.GetString(key, "{}");
                return JsonUtility.FromJson(json, type);
            }
        }
        
        private static object GetDefaultValue(Type type, PlayerPrefsAttribute attribute)
        {
            if (type == typeof(int) || type == typeof(int?))
            {
                return (int)attribute.DefaultValue;
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return attribute.DefaultValue;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return attribute.DefaultBool;
            }
            else if (type == typeof(string))
            {
                return attribute.DefaultString;
            }
            else if (type == typeof(Vector2))
            {
                return Vector2.zero;
            }
            else if (type == typeof(Vector3))
            {
                return Vector3.zero;
            }
            else if (type == typeof(Color))
            {
                return Color.white;
            }
            else if (type.IsEnum)
            {
                return Enum.ToObject(type, 0);
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.MinValue;
            }
            else
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
