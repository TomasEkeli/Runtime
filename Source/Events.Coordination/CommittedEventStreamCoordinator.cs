﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Publishing;

namespace Dolittle.Runtime.Events.Coordination
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamCoordinator"/>
    /// </summary>
    [Singleton]
    public class CommittedEventStreamCoordinator : ICommittedEventStreamCoordinator
    {
        readonly ICanReceiveCommittedEventStream _committedEventStreamReceiver;
        readonly IEventProcessors _eventProcessors;
        readonly IEventProcessorLog _eventProcessorLog;
        readonly IEventProcessorStates _eventProcessorStates;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="committedEventStreamReceiver"><see cref="ICanReceiveCommittedEventStream">Committed event stream receiver</see> for receiving events</param>
        /// <param name="eventProcessors"></param>
        /// <param name="eventProcessorLog"></param>
        /// <param name="eventProcessorStates"></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public CommittedEventStreamCoordinator(
            ICanReceiveCommittedEventStream committedEventStreamReceiver,
            IEventProcessors eventProcessors,
            IEventProcessorLog eventProcessorLog,
            IEventProcessorStates eventProcessorStates, 
            ILogger logger)
        {
            _committedEventStreamReceiver = committedEventStreamReceiver;
            _eventProcessors = eventProcessors;
            _eventProcessorLog = eventProcessorLog;
            _eventProcessorStates = eventProcessorStates;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _committedEventStreamReceiver.Received += CommittedEventStreamReceived;
        }

        void CommittedEventStreamReceived(CommittedEventStream committedEventStream)
        {
            _logger.Information("Committed event stream received - coordinating it");
            committedEventStream.ForEach(e =>
            {
                _logger.Trace("Event received");
               
                var results = _eventProcessors.Process(e.Envelope, e.Contents);
                Parallel.ForEach(results, result =>
                {
                    if (result.Status == EventProcessingStatus.Success)
                    {
                        _logger.Trace("Event processing succeeded");
                        _eventProcessorStates.ReportSuccessFor(result.EventProcessor, e.Contents, e.Envelope);

                        if( result.Messages.Count() > 0 )
                        {
                            result.Messages.ForEach(message => _logger.Trace($"Event processor message : '{message.Message}' with severity '{message.Severity}'"));
                            _eventProcessorLog.Info(result.EventProcessor, e.Contents, e.Envelope, result.Messages);
                        }
                    }
                    else
                    {
                        _logger.Trace("Event processing failed");
                        _eventProcessorStates.ReportFailureFor(result.EventProcessor, e.Contents, e.Envelope);
                        _eventProcessorLog.Failed(result.EventProcessor, e.Contents, e.Envelope, result.Messages);
                    }
                });
            });
        }
    }
}
