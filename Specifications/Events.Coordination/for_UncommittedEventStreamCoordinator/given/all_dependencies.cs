﻿using Dolittle.Runtime.Events.Storage;
using Dolittle.Runtime.Events.Publishing;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Events.Coordination.Specs.for_UncommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<IEventStore> event_store;
        protected static Mock<IEventSourceVersions> event_source_versions;
        protected static Mock<ICanSendCommittedEventStream> committed_event_stream_sender;
        protected static Mock<IEnvelopes> event_envelopes;
        protected static Mock<ISequenceNumbers> event_sequence_numbers;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            event_store = new Mock<IEventStore>();
            event_source_versions = new Mock<IEventSourceVersions>();
            committed_event_stream_sender = new Mock<ICanSendCommittedEventStream>();
            event_envelopes = new Mock<IEnvelopes>();
            event_sequence_numbers = new Mock<ISequenceNumbers>();
            logger = new Mock<ILogger>();
        };
    }
}
