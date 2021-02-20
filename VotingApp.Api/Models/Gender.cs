namespace VotingApp.Api.Models
{
    public static class Gender
    {
        public static string Male => GenderType.Male.ToString();

        public static string Female => GenderType.Female.ToString();

        public enum GenderType
        {
            Male, Female
        }
    }
}