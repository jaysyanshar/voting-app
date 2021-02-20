namespace VotingApp.Api.Models
{
    public static class Gender
    {
        public static string Male => Enum.Male.ToString();

        public static string Female => Enum.Female.ToString();

        public enum Enum
        {
            Male, Female
        }
    }
}