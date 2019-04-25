using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Validators
{
    public class UserValidator: IValidator<User>
    {
        public bool Validate(User entity)
        {
            return !(entity == null || string.IsNullOrEmpty(entity.LastName) || entity.LastName.Length > 50 ||
                     (entity.FirstName != null && entity.FirstName.Length > 50));
        }
    }
}
