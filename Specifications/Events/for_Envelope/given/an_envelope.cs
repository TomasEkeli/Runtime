using System;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Events.Specs.for_Envelope.given
{
    public class an_envelope
    {
        protected static IEnvelope envelope;
        protected static Mock<IApplicationArtifactIdentifier> event_identifier;
        protected static Mock<IApplicationArtifactIdentifier> event_source_identifier;
        protected static EventSourceId event_source_id;
        protected static EventSourceVersion version;

        Establish context = () =>
        {
            event_source_id = EventSourceId.New();
            event_identifier = new Mock<IApplicationArtifactIdentifier>();
            event_source_identifier = new Mock<IApplicationArtifactIdentifier>();
            version = EventSourceVersion.Zero;
            envelope = new Envelope(
                CorrelationId.NotSet,
                EventId.New(),
                SequenceNumber.Zero,
                GenerationOfEvent.First,
                event_identifier.Object,
                event_source_id,
                event_source_identifier.Object,
                version,
                CausedBy.Unknown,
                DateTimeOffset.UtcNow
            );
        };

    }
}
