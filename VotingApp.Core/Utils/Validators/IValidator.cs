namespace VotingApp.Core.Utils.Validators
{
    public interface IValidator<in T>
    {
        bool Validate( T data );
    }
}