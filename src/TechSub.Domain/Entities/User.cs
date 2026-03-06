using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSub.Domain.Enuns;

namespace TechSub.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }

        private User() { }

        public User(string name, string email, string passwordHash, UserRole role = UserRole.Customer)
            : base() 
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdateTimestamp();
        }

        public void PromoteToAdmin()
        {
            Role = UserRole.Admin;
            UpdateTimestamp();
        }
    }
}
