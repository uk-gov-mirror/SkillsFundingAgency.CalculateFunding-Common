﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateFunding.Common.ServiceBus.Interfaces
{
    public interface IMessengerService
    {
        string ServiceName { get; }

        Task<(bool Ok, string Message)> IsHealthOk(string queueName);

        Task<IEnumerable<T>> ReceiveMessages<T>(string entityPath, TimeSpan timeout) where T : class;

        Task<T> ReceiveMessage<T>(string entityPath, Predicate<T> predicate, TimeSpan timeout) where T : class;

        Task SendToQueue<T>(string queueName, T data, IDictionary<string, string> properties, bool compressData = false, string sessionId = null) where T : class;

        Task SendToQueueAsJson(string queueName, string data, IDictionary<string, string> properties, bool compressData = false, string sessionId = null);

        Task SendToTopic<T>(string topicName, T data, IDictionary<string, string> properties, bool compressData = false, string sessionId = null) where T : class;

        Task SendToTopicAsJson(string topicName, string data, IDictionary<string, string> properties, bool compressData = false, string sessionId = null);
    }
}