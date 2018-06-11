/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents the identification of an <see cref="IEvent"/>
    /// </summary>
    public class SequenceNumber : ConceptAs<ulong>
    {
        /// <summary>
        /// Represents a null Event - EventId *MUST* start with 1
        /// </summary>
        public static SequenceNumber Zero = 0L;

        /// <summary>
        /// Implicitly convert from a <see cref="ulong"/> to an <see cref="SequenceNumber"/>
        /// </summary>
        /// <param name="sequenceNumber">Actual sequence number</param>
        public static implicit operator SequenceNumber(ulong sequenceNumber)
        {
            return new SequenceNumber { Value = sequenceNumber };
        }
    }
}
