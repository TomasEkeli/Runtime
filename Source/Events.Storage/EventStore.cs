/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Collections;
using Dolittle.Serialization.Protobuf;

namespace Dolittle.Runtime.Events.Storage
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore"/>
    /// </summary>
    public class EventStore : IEventStore
    {
        readonly IEventStorage _storage;
        readonly ISerializer _serializer;
        private readonly IEventStoragePaths _paths;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="paths"></param>
        /// <param name="serializer"></param>
        public EventStore(IEventStorage storage, IEventStoragePaths paths, ISerializer serializer)
        {
            _serializer = serializer;
            _storage = storage;
            _paths = paths;
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<Letter> letters)
        {
            var context = new EventStorageContext();

            letters.ForEach(_ =>
            {
                var paths = _paths.GetForContext(_.Envelope.EventSourceId);
                paths.ForEach(path =>
                {
                    var stream = _storage.GetAppendStreamFor(path);
                    _serializer.ToProtobuf(_.Envelope, stream, includeLength: true);
                    _serializer.ToProtobuf(_.Contents, stream, includeLength: true);
                });
            });
        }

        /// <inheritdoc/>
        public IEnumerable<Letter> GetFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationArtifactIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new System.NotImplementedException();
        }
    }
}