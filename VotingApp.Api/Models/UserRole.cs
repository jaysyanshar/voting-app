namespace VotingApp.Api.Models
{
    public static class UserRole
    {
        public static string Admin
        {
            get { return Type.Admin.ToString(); }
        }

        public static string Client
        {
            get { return Type.Client.ToString(); }
        }

        public enum Type
        {
            Undefined = default,
            Admin,
            Client
        }
    }
}