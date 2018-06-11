/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Applications;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Storage
{
    /// <summary>
    /// Defines a system for working with <see cref="SequenceNumber">event sequence numbers</see>
    /// </summary>
    public interface ISequenceNumbers
    {
        /// <summary>
        /// Allocate the next global <see cref="SequenceNumber"/>
        /// </summary>
        /// <returns>The next <see cref="SequenceNumber"/></returns>
        SequenceNumber Next();
    }
}
