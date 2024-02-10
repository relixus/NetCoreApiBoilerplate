namespace NetCoreApiBoilerplate.Middlewares.CustomAuthorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequireClaimAttribute : Attribute
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public RequireClaimAttribute(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
