﻿using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;
using System;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Name { get; protected set; }

        public string Salt { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

       protected User()
        {

        }

        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("empy_user_email", "User email cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empy_user_name", "User name cannot be empty");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password,IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("empy_user_password", "User password cannot be empty");
            }

            Salt = encrypter.GetSalt(password);

            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
        => password.Equals(encrypter.GetHash(password, Salt));
    }
}
