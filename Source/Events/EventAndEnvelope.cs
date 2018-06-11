/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents an <see cref="Events.Envelope"/> and an <see cref="IEvent"/>
    /// </summary>
    public class EventAndEnvelope
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventAndEnvelope"/>
        /// </summary>
        /// <param name="envelope"><see cref="IEnvelope">Envelope</see> with metadata for the <see cref="IEvent"/></param>
        /// <param name="theEvent"><see cref="IEvent">Event</see> that is represented</param>
        public EventAndEnvelope(IEnvelope envelope, IEvent theEvent)
        {
            Envelope = envelope;
            Event = theEvent;
        }

        /// <summary>
        /// Gets the <see cref="Events.Envelope">envelope</see>
        /// </summary>
        public IEnvelope    Envelope { get; }

        /// <summary>
        /// Gets the <see cref="IEvent">event</see>
        /// </summary>
        public IEvent Event { get; }
    }
}
