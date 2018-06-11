﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEnvelope"/>; the envelope for the event with all the metadata related to the event
    /// </summary>
    public class Envelope : IEnvelope
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Envelope"/>
        /// </summary>
        /// <param name="correlationId"><see cref="CorrelationId"/> the <see cref="IEvent"/> is part of</param>
        /// <param name="eventId"><see cref="EventId"/> for the <see cref="IEvent"/></param>
        /// <param name="sequenceNumber"></param>
        /// <param name="generation"><see cref="GenerationOfEvent"/> for the <see cref="IEvent"/> </param>
        /// <param name="event"><see cref="IApplicationArtifactIdentifier"/> representing the <see cref="IEvent"/></param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> for the <see cref="IEventSource"/></param>
        /// <param name="eventSource"><see cref="IApplicationArtifactIdentifier"/> representing the <see cref="IEventSource"/></param>
        /// <param name="version"><see cref="EventSourceVersion">Version</see> of the event related to the <see cref="IEventSource"/></param>
        /// <param name="causedBy"><see cref="string"/> representing which person or what system caused the event</param>
        /// <param name="occurred"><see cref="DateTime">When</see> the event occured</param>
        public Envelope(
            CorrelationId correlationId,
            EventId eventId,
            SequenceNumber sequenceNumber,
            GenerationOfEvent generation, 
            IApplicationArtifactIdentifier @event, 
            EventSourceId eventSourceId, 
            IApplicationArtifactIdentifier eventSource, 
            EventSourceVersion version, 
            CausedBy causedBy, 
            DateTimeOffset occurred)
        {
            CorrelationId = correlationId;
            EventId = eventId;
            SequenceNumber = sequenceNumber;
            Generation = generation;
            Event = @event;
            EventSourceId = eventSourceId;
            EventSource = eventSource;
            Version = version;
            CausedBy = causedBy;
            Occurred = occurred;
        }

        /// <inheritdoc/>
        public CorrelationId CorrelationId { get; }

        /// <inheritdoc/>
        public EventId EventId { get; }

        /// <inheritdoc/>
        public SequenceNumber SequenceNumber { get; }

        /// <inheritdoc/>
        public GenerationOfEvent Generation { get; }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier Event { get; }

        /// <inheritdoc/>
        public EventSourceId EventSourceId { get; }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier EventSource { get; }

        /// <inheritdoc/>
        public EventSourceVersion Version { get; }

        /// <inheritdoc/>
        public CausedBy CausedBy { get; }

        /// <inheritdoc/>
        public DateTimeOffset Occurred { get; }

        /// <inheritdoc/>
        public IEnvelope WithSequenceNumber(SequenceNumber sequenceNumber)
        {
            return new Envelope(CorrelationId, EventId, sequenceNumber, Generation, Event, EventSourceId, EventSource, Version, CausedBy, Occurred);
        }

        /// <inheritdoc/>
        public IEnvelope WithCorrelationId(CorrelationId correlationId)
        {
            return new Envelope(correlationId, EventId, SequenceNumber, Generation, Event, EventSourceId, EventSource, Version, CausedBy, Occurred);
        }
    }
}
