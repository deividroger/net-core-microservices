﻿using Actio.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            await Task.CompletedTask;

            Console.WriteLine($"User Authenticated: {@event.Email}.");
        }
    }
}
