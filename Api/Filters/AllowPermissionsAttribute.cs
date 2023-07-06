namespace TestApp.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AllowPermissionsAttribute:Attribute
    {
        public AllowPermissionsAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
    
        public string[] Permissions { get; set; }
    }
}
