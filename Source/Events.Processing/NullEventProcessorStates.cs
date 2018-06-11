/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventProcessorStates"/>
    /// </summary>
    public class NullEventProcessorStates : IEventProcessorStates
    {
         
        /// <ineritdoc/>
        public IEventProcessorState GetFor(IEventProcessor eventProcessor)
        {
            return new EventProcessorState(
                    eventProcessor,
                    EventProcessorStatus.Online,
                    DateTimeOffset.MaxValue,
                    SequenceNumber.Zero,
                    SequenceNumber.Zero,
                    EventProcessingStatus.Success
                );
        }

        /// <ineritdoc/>
        public void ReportFailureFor(IEventProcessor eventProcessor, IEvent @event, IEnvelope envelope)
        {
            
        }

        /// <ineritdoc/>
        public void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEnvelope envelope)
        {
            
        }
    }
}
