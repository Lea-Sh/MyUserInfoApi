using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyUserInfoAPI.Models
{
    public partial class User
    {
        public override string ToString()
        {
            return $"User: ID: {this.UserId}, First Name: {this.FirstName ?? ""}, Last Name: {this.LastName}";
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User u = obj as User;
            if ((System.Object)u == null)
            {
                return false;
            }

           return (UserId == u.UserId) && FirstName.Equals(u.FirstName) && LastName.Equals(u.LastName);
        }

        public bool Equals(User u)
        {
            if ((object) u == null)
            {
                return false;
            }

            return (UserId == u.UserId) && FirstName.Equals(u.FirstName) && LastName.Equals(u.LastName);
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode() + FirstName.GetHashCode() + LastName.GetHashCode();
        }
    }
}
