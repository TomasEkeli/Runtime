/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Defines the envelope for the event with all the metadata related to the event
    /// </summary>
    public interface IEnvelope
    {
        /// <summary>
        /// Gets the <see cref="CorrelationId"/> that the <see cref="IEvent"/> is part of
        /// </summary>
        CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="EventId"/> representing the <see cref="IEvent"/>s
        /// </summary>
        EventId EventId { get; }

        /// <summary>
        /// Gets the global sequence number used in the event store
        /// </summary>
        SequenceNumber SequenceNumber { get; }

        /// <summary>
        /// Gets the <see cref="GenerationOfEvent"/> for the <see cref="IEvent"/>
        /// </summary>
        GenerationOfEvent Generation { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationArtifactIdentifier">identifier</see> identifying the <see cref="IEvent"/>
        /// </summary>
        IApplicationArtifactIdentifier Event { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceId">id</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceId EventSourceId { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationArtifactIdentifier">identifier</see> identifying the <see cref="IEventSource"/>
        /// </summary>
        IApplicationArtifactIdentifier EventSource { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceVersion">version</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceVersion Version { get; }

        /// <summary>
        /// Gets who or what the event was caused by.
        /// 
        /// Typically this would be the name of the user or system causing it
        /// </summary>
        CausedBy CausedBy { get; }

        /// <summary>
        /// Gets the time the event occurred
        /// </summary>
        DateTimeOffset Occurred { get; }

        /// <summary>
        /// Creates a new <see cref="Envelope"/> with a different <see cref="CorrelationId">correlation id</see>
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns>A copy of the <see cref="Envelope"/> with a new <see cref="CorrelationId"/> </returns>
        IEnvelope WithCorrelationId(CorrelationId correlationId);

        /// <summary>
        /// Creates a new <see cref="Envelope"/> with a different <see cref="Events.SequenceNumber">sequence number</see>
        /// </summary>
        /// <param name="sequenceNumber">The new <see cref="Events.SequenceNumber"/></param>
        /// <returns>A copy of the <see cref="Envelope"/> with a new Id </returns>
        IEnvelope WithSequenceNumber(SequenceNumber sequenceNumber);
    }
}