﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Execution;
using Dolittle.Runtime.Transactions;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Storage;
using Dolittle.Runtime.Events.Publishing;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Coordination
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        IEventStore _eventStore;
        IEventSourceVersions _eventSourceVersions;
        ICanSendCommittedEventStream _committedEventStreamSender;
        IEnvelopes _envelopes;
        ISequenceNumbers _sequenceNumbers;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use for saving the events</param>
        /// <param name="eventSourceVersions"><see cref="IEventSourceVersions"/> for working with the version for the <see cref="IEventSource"/></param>
        /// <param name="committedEventStreamSender"><see cref="ICanSendCommittedEventStream"/> send the <see cref="CommittedEventStream"/></param>
        /// <param name="eventEnvelopes"><see cref="IEnvelopes"/> for working with <see cref="Envelope"/></param>
        /// <param name="sequenceNumbers"><see cref="ISequenceNumbers"/> for allocating <see cref="SequenceNumber">sequence number</see> for <see cref="IEvent">events</see></param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            IEventSourceVersions eventSourceVersions,
            ICanSendCommittedEventStream committedEventStreamSender,
            IEnvelopes eventEnvelopes,
            ISequenceNumbers sequenceNumbers,
            ILogger logger)
        {
            _eventStore = eventStore;
            _eventSourceVersions = eventSourceVersions;
            _committedEventStreamSender = committedEventStreamSender;
            _envelopes = eventEnvelopes;
            _sequenceNumbers = sequenceNumbers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Commit(TransactionCorrelationId correlationId, UncommittedEventStream uncommittedEventStream)
        {
            _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
            var envelopes = _envelopes.CreateFrom(uncommittedEventStream.EventSource, uncommittedEventStream.VersionedEvents);
            var envelopesAsArray = envelopes.ToArray();
            var eventsAsArray = uncommittedEventStream.ToArray();

            _logger.Trace("Create an array of events and envelopes");
            var letters = new List<Letter>();
            for( var eventIndex=0; eventIndex<eventsAsArray.Length; eventIndex++ )
            {
                var envelope = envelopesAsArray[eventIndex];
                var @event = eventsAsArray[eventIndex];
                letters.Add(new Letter(
                    envelope
                        .WithTransactionCorrelationId(correlationId)
                        .WithSequenceNumber(_sequenceNumbers.Next()),
                    @event
                ));
            }

            _logger.Trace("Committing events to event store");
            _eventStore.Commit(letters);

            _logger.Trace($"Set event source versions for the event source '{envelopesAsArray[0].EventSource}' with id '{envelopesAsArray[0].EventSourceId}'");
            _eventSourceVersions.SetFor(envelopesAsArray[0].EventSource, envelopesAsArray[0].EventSourceId, envelopesAsArray[envelopesAsArray.Length - 1].Version);

            _logger.Trace("Create a committed event stream");
            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, letters);

            _logger.Trace("Send the committed event stream");
            _committedEventStreamSender.Send(committedEventStream);
        }
    }
}
