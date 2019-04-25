namespace MyUserInfoAPI.Validators
{
    public interface IValidator<T>
    {
        bool Validate(T entity);
    }
}
