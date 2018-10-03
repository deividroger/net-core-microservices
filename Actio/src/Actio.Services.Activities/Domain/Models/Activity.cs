﻿using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Category { get; protected set; }

        public string Description { get; protected set; }

        public Guid UserId { get; protected set; }

        public DateTime CreateAt { get; protected set; }

        protected Activity()
        {
        }

        protected Activity(Guid id, Category category,Guid userId,
                           string name, string description,DateTime createAt )
        {
            Id = id;
            Category = category.Name;
            Name = name;
            Description = description;
            CreateAt = createAt;

        }
    }
}
