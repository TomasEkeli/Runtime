/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents the generation of an <see cref="IEvent"/>
    /// </summary>
    public class GenerationOfEvent : ConceptAs<int>
    {
        /// <summary>
        /// First generation of an event
        /// </summary>
        public static GenerationOfEvent First = 1;

        /// <summary>
        /// Implicitly convert from a <see cref="int"/> to an <see cref="GenerationOfEvent"/>
        /// </summary>
        /// <param name="generation">The generation</param>
        public static implicit operator GenerationOfEvent(int generation)
        {
            return new GenerationOfEvent { Value = generation };
        }
    }
}
