/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents a special version of an eventstream
    /// that holds committed <see cref="IEvent">events</see>
    /// </summary>
    public class CommittedEventStream : IEnumerable<Letter>
    {
        List<Letter> _events = new List<Letter>();


        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStream">CommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        /// <param name="letters">The <see cref="IEvent">events</see> with their <see cref="Envelope">envelopes</see></param>
        public CommittedEventStream(EventSourceId eventSourceId, IEnumerable<Letter> letters)
        {
            EventSourceId = eventSourceId;
            foreach (var eventAndEnvelope in letters)
            {
                EnsureEventIsValid(eventAndEnvelope);
                _events.Add(eventAndEnvelope);
            }
        }

        /// <summary>
        /// Gets the Id of the <see cref="IEventSource"/> that this <see cref="CommittedEventStream"/> relates to.
        /// </summary>
        public EventSourceId EventSourceId { get; private set; }

        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return Count > 0; }
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return _events.Count; }
        }

        /// <summary>
        /// Get a generic enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Letter> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void EnsureEventIsValid(Letter letter)
        {
            if (letter.Contents == null)
                throw new ArgumentNullException("Cannot append a null event");
        }
    }
}