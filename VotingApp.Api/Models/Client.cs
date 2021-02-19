namespace VotingApp.Api.Models
{
    public class Client : IModel<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public string GetKey()
        {
            return Email;
        }
    }
}