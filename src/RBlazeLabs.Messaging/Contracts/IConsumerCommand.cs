﻿using MassTransit;

namespace RBlazeLabs.Messaging.Contracts
{

    /// <summary>
    /// Consumer command interface contract
    /// </summary>
    /// <typeparam name="TMessage">Message command type</typeparam>
    public interface IConsumerCommand<TMessage> : IConsumer<TMessage>
        where TMessage : class, IMessageCommand
    {
    }
}
