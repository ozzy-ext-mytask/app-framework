﻿using System;
using System.Threading.Tasks;
using MyLab.ProtocolStorage.Client;
using MyLab.TaskApp.IterationContext;

namespace MyLab.TaskApp.Protocol
{
    interface IProtocolWriter
    {
        Task WriteAsync(TaskIterationContext ctx, TimeSpan iterationDuration, Exception error = null);
    }

    class ProtocolWriter : IProtocolWriter
    {
        private readonly SafeProtocolIndexerV1 _indexer;
        private readonly string _protocolId;

        public TaskKicker TaskKicker { get; set; }

        public ProtocolWriter(SafeProtocolIndexerV1 indexer, string protocolId)
        {
            _indexer = indexer;
            _protocolId = protocolId;
        }
        public Task WriteAsync(TaskIterationContext ctx, TimeSpan iterationDuration, Exception error = null)
        {
            return _indexer.PostEventAsync(_protocolId, new TaskIterationProtocolEvent
            {
                Id = ctx.Report?.CorrelationId,
                DateTime = ctx.StartAt,
                Metrics = ctx.Report?.Metrics,
                Subject = ctx.Report?.SubjectId,
                Type = ProtocolEventConstants.Type,
                TraceId = ctx.Id,
                Kicker = TaskKicker,
                Workload = ctx.Report?.Workload ?? IterationWorkload.Undefined,
                Duration = iterationDuration,
                Error = error
            });
        }
    }
}