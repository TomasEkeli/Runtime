/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents an exceptional situation where an event source is stateful 
    /// but there has been an attempt to retrieve it without restoring state by replaying events (fast-forwarding)
    /// </summary>
    public class InvalidFastForwardOfEventSource : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardOfEventSource">exception for when an event source cannot be fast forwarded</see>
        /// </summary>
        /// <param name="message">Exception Message</param>
        public InvalidFastForwardOfEventSource(string message)
            : base(message)
        {
        }
    }
}