/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications;

namespace Dolittle.Runtime.Events.Storage
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventStore"/>
    /// </summary>
    public class NullEventStore : IEventStore
    {
        /// <inheritdoc/>
        public IEnumerable<Letter> GetFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            return new Letter[0];
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<Letter> eventsAndEnvelopes)
        {
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            return false;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            return EventSourceVersion.Zero;
        }
    }
}
