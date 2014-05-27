﻿using System;
using System.Collections.Generic;
using System.Threading;
using IronSharp.Core;
using Newtonsoft.Json;

namespace IronSharp.IronMQ
{
    public class QueueInfo : IInspectable
    {
        [JsonProperty("subscribers", DefaultValueHandling = DefaultValueHandling.Ignore)] 
        private List<Subscriber> _subscribers;
        
        [JsonProperty("alerts", DefaultValueHandling = DefaultValueHandling.Ignore)] 
        private List<Alert> _alerts;

        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("project_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProjectId { get; set; }

        /// <summary>
        /// Either multicast to push to all subscribers or unicast to push to one and only one subscriber.
        /// Default is multicast.
        /// To revert push queue to reqular pull queue set pull.
        /// </summary>
        [JsonIgnore]
        public PushType PushType { get; set; }

        /// <summary>
        /// How many times to retry on failure.
        /// Default is 3.
        /// Maximum is 100.
        /// </summary>
        [JsonProperty("retries", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Retries { get; set; }

        /// <summary>
        /// Delay between each retry in seconds.
        /// Default is 60.
        /// </summary>
        [JsonProperty("retries_delay", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? RetriesDelay { get; set; }

        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Size { get; set; }

        [JsonProperty("total_messages", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? TotalMessages { get; set; }

        [JsonProperty("push_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        protected string PushTypeValue
        {
            get
            {
                switch (PushType)
                {
                    case PushType.Pull:
                        return "push";
                    case PushType.Multicast:
                        return "multicast";
                    case PushType.Unicast:
                        return "unicast";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set { PushType = value.As<PushType>(); }
        }

        /// <summary>
        /// Time (in seconds), after which messages taken from this queue will be placed back onto queue.
        /// You must delete the message from the queue to ensure it does not go back onto the queue.
        /// Default is 60 seconds.
        /// Minimum is 30 seconds, and maximum is 86,400 seconds (24 hours).
        /// </summary>
        [JsonProperty("timeout", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Timeout { get; set; }

        /// <summary>
        /// Time (in seconds) after posting the message to the queue it will be deleted
        /// Default is 604800 seconds (7 days).
        /// </summary>
        [JsonProperty("expire_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ExpireTime { get; set; }

        [JsonIgnore]
        public List<Subscriber> Subscribers
        {
            get { return LazyInitializer.EnsureInitialized(ref _subscribers); }
            set { _subscribers = value; }
        }

        public List<Alert> Alerts
        {
            get { return LazyInitializer.EnsureInitialized(ref _alerts); }
            set { _alerts = value; }
        }
    }
}