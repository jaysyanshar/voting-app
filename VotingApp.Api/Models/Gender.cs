namespace VotingApp.Api.Models
{
    public static class Gender
    {
        public static string Male
        {
            get { return Type.Male.ToString(); }
        }

        public static string Female
        {
            get { return Type.Female.ToString(); }
        }

        public enum Type
        {
            Undefined = default,
            Male,
            Female
        }
    }
}