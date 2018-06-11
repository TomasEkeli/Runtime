/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Applications;

namespace Dolittle.Runtime.Events.Storage
{
    /// <summary>
    /// Represents a null implementation of <see cref="ISequenceNumbers"/>
    /// </summary>
    public class NullSequenceNumbers : ISequenceNumbers
    {
        /// <inheritdoc/>
        public SequenceNumber Next()
        {
            return 0L;
        }

        /// <inheritdoc/>
        public SequenceNumber NextForType(IApplicationArtifactIdentifier identifier)
        {
            return 0L;
        }
    }
}
