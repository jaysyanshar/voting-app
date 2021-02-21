namespace VotingApp.Core.Models
{
    public interface IModel<out TKey> : IModel
    {
        TKey GetKey();
    }

    public interface IModel
    {
        bool ValidateFields();
    }
}