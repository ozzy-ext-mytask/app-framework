﻿using System.Collections.Generic;
using MyLab.ProtocolStorage.Client.Models;
using MyLab.TaskApp.IterationContext;
using Newtonsoft.Json;

namespace MyLab.TaskApp.Protocol
{
    class TaskIterationProtocolEvent : ProtocolEvent
    {
        [JsonProperty("workload")]
        [JsonConverter(typeof(TaskEnumJsonConverter))]
        public IterationWorkload Workload { get; set; }

        [JsonProperty("metrics")]
        public IDictionary<string, double> Metrics { get; set; }

        [JsonProperty("kicker")]
        [JsonConverter(typeof(TaskEnumJsonConverter))]
        public TaskKicker Kicker { get; set; }
    }
}
