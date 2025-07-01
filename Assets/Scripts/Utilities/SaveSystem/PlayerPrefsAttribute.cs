using System;

namespace Utilities.SaveBox
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PlayerPrefsAttribute : Attribute
    {
        public string Key { get; private set; }
        
        public string KeyPrefix { get; private set; }
        
        public float DefaultValue { get; private set; }
        
        public string DefaultString { get; private set; }
       
        public bool DefaultBool { get; private set; }
        
        public PlayerPrefsAttribute(string key = null)
        {
            Key = key;
            KeyPrefix = "";
            DefaultValue = 0f;
            DefaultString = "";
            DefaultBool = false;
        }
        
        public PlayerPrefsAttribute(string keyPrefix, string key)
        {
            Key = key;
            KeyPrefix = keyPrefix;
            DefaultValue = 0f;
            DefaultString = "";
            DefaultBool = false;
        }
        
        public PlayerPrefsAttribute(string key, float defaultValue)
        {
            Key = key;
            KeyPrefix = "";
            DefaultValue = defaultValue;
            DefaultString = "";
            DefaultBool = false;
        }
        
        public PlayerPrefsAttribute(string key, bool defaultBool)
        {
            Key = key;
            KeyPrefix = "";
            DefaultValue = 0f;
            DefaultString = "";
            DefaultBool = defaultBool;
        }
        
        public PlayerPrefsAttribute(string keyPrefix, string key, float defaultValue)
        {
            Key = key;
            KeyPrefix = keyPrefix;
            DefaultValue = defaultValue;
            DefaultString = "";
            DefaultBool = false;
        }
        
        public PlayerPrefsAttribute(string keyPrefix, string key, bool defaultBool)
        {
            Key = key;
            KeyPrefix = keyPrefix;
            DefaultValue = 0f;
            DefaultString = "";
            DefaultBool = defaultBool;
        }
        
        public PlayerPrefsAttribute(string keyPrefix, string key, string defaultString)
        {
            Key = key;
            KeyPrefix = keyPrefix;
            DefaultValue = 0f;
            DefaultString = defaultString;
            DefaultBool = false;
        }
        
        public string GetFullKey(string fieldName)
        {
            string key = Key ?? fieldName;
            return string.IsNullOrEmpty(KeyPrefix) ? key : $"{KeyPrefix}.{key}";
        }
    }
}