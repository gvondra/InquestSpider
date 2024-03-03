namespace ResourceRPC
{
    public static class Constants
    {
        public const string AUTH_SCHEME_GOOGLE = "GoogleAuthentication";
        public const string AUTH_SCHEMA_INQUESTSPIDER = "InquestSpiderAuthentication";
        public const string POLICY_BL_AUTH = "BL:AUTH"; // ensures the requestor used a brass loon token
        public const string POLICY_TOKEN_CREATE = "TOKEN:CREATE";
        public const string POLICY_RESOURCE_READ = "RESOURCE:READ";
        public const string POLICY_RESOURCE_EDIT = "RESOURCE:EDIT";
    }
}
