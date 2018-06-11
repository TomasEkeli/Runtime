/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Runtime.Transactions
{
    /// <summary>
    /// Represents a uniquely identifiable correlation id associated with a transaction
    /// </summary>
    public class CorrelationId : ConceptAs<Guid>
    {
        /// <summary>
        /// Creates a new instance of <see cref="CorrelationId"/> with a unique id
        /// </summary>
        /// <returns>A new <see cref="CorrelationId"/></returns>
        public static CorrelationId New()
        {
            return new CorrelationId { Value = Guid.NewGuid() };
        }

        /// <summary>
        /// Gets the value representing a not set <see cref="CorrelationId"/>
        /// </summary>
        public static CorrelationId NotSet = Guid.Empty;

        /// <summary>
        /// Implicitly convert from a <see cref="Guid"/> to a <see cref="CorrelationId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> for the value</param>
        public static implicit operator CorrelationId(Guid value)
        {
            return new CorrelationId { Value = value };
        }
    }
}
