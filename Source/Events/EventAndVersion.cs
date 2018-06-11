﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents an <see cref="IEvent"/> and the <see cref="EventSourceVersion">version</see> on its <see cref="IEventSource"/>
    /// </summary>
    public class VersionedEvent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VersionedEvent"/>
        /// </summary>
        /// <param name="event">The <see cref="IEvent">Event</see></param>
        /// <param name="version">The <see cref="EventSourceVersion">version</see> of the <see cref="IEvent"/> on the <see cref="IEventSource"/></param>
        public VersionedEvent(IEvent @event, EventSourceVersion version)
        {
            Event = @event;
            Version = version;
        }

        /// <summary>
        /// Gets the <see cref="IEvent">event</see>
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceVersion">version</see>
        /// </summary>
        public EventSourceVersion Version { get; }
    }
}
