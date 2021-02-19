namespace VotingApp.Api.Models
{
    public interface IModel<out TKey> : IModel
    {
        TKey GetKey();
    }

    public interface IModel { }
}