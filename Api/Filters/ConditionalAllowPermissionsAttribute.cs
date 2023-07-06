namespace TestApp.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class ConditionalAllowPermissionsAttribute:Attribute
    {
        public ConditionalAllowPermissionsAttribute(string argumentName, object value, string[] permissions) { 
        
            ArgumentName = argumentName;
            Value = value;
            Permissions = permissions;
        }
        public string ArgumentName { get; set; }
        public object Value { get; set; }
        public string[] Permissions { get; set; }
    }
}
