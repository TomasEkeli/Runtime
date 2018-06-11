/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Storage
{
    /// <summary>
    /// Defines a system for working with <see cref="Envelope"/>
    /// </summary>
    public interface IEnvelopes
    {
        /// <summary>
        /// Create a <see cref="Envelope"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to create <see cref="Envelope"/> from</param>
        /// <param name="event"><see cref="IEvent"/> to create <see cref="Envelope"/> from</param>
        /// <param name="version"><see cref="EventSourceVersion">Version</see> of the <see cref="IEvent"/> on an <see cref="IEventSource"/></param>
        /// <returns><see cref="IEnvelope"/></returns>
        IEnvelope CreateFrom(IEventSource eventSource, IEvent @event, EventSourceVersion version);

        /// <summary>
        /// Create an <see cref="IEnumerable{IEnvelope}"/> from <see cref="IEnumerable{EventAndVersion}"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to create from</param>
        /// <param name="eventsAndVersion"><see cref="IEnumerable{EventAndVersion}">Events and version</see> to create from</param>
        /// <returns><see cref="IEnumerable{IEnvelope}">Event envelopes</see></returns>
        IEnumerable<IEnvelope> CreateFrom(IEventSource eventSource, IEnumerable<EventAndVersion> eventsAndVersion);
    }
}
