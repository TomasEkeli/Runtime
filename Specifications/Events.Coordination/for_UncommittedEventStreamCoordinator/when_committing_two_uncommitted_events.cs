using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Coordination.Specs.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(UncommittedEventStreamCoordinator))]
    public class when_committing_two_uncommitted_events : given.an_uncommitted_event_stream_coordinator
    {
        static SequenceNumber first_event_sequence_number = 42L;
        static SequenceNumber first_event_sequence_number_for_type = 43L;

        static SequenceNumber second_event_sequence_number = 44L;
        static SequenceNumber second_event_sequence_number_for_type = 45L;

        static string sequence_string = string.Empty;

        static TransactionCorrelationId transaction_correlation_id;

        static IEnumerable<Letter> uncommitted_events;
        static UncommittedEventStream uncommitted_event_stream;

        static CommittedEventStream committed_event_stream;

        static Mock<IEventSource> event_source;
        static EventSourceId event_source_id = Guid.NewGuid();
        static Mock<IApplicationArtifactIdentifier> event_source_identifier;

        static Mock<IEvent> first_event;
        static IEnvelope first_event_envelope;
        static Mock<IApplicationArtifactIdentifier> first_event_identifier;
        static EventSourceVersion first_event_source_version;

        static Mock<IEvent> second_event;
        static IEnvelope second_event_envelope;
        static Mock<IApplicationArtifactIdentifier> second_event_identifier;
        static EventSourceVersion second_event_source_version;

        Establish context = ()=>
        {
            transaction_correlation_id = Guid.NewGuid();

            event_source_identifier = new Mock<IApplicationArtifactIdentifier>();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);

            first_event_source_version = new EventSourceVersion(4, 2);
            first_event_identifier = new Mock<IApplicationArtifactIdentifier>();
            first_event = new Mock<IEvent>();
            first_event_envelope = new Envelope(
                TransactionCorrelationId.NotSet,
                EventId.New(),
                SequenceNumber.Zero,
                EventGeneration.First,
                first_event_identifier.Object,
                event_source_id,
                event_source_identifier.Object,
                first_event_source_version,
                CausedBy.Unknown,
                DateTimeOffset.UtcNow
            );
            var first_event_and_envelope = new Letter(first_event_envelope, first_event.Object);
            var first_event_and_version = new VersionedEvent(first_event.Object, first_event_source_version);

            second_event_source_version = new EventSourceVersion(4, 3);
            second_event_identifier = new Mock<IApplicationArtifactIdentifier>();
            second_event = new Mock<IEvent>();
            second_event_envelope = new Envelope(
                TransactionCorrelationId.NotSet,
                EventId.New(),
                SequenceNumber.Zero,
                EventGeneration.First,
                first_event_identifier.Object,
                event_source_id,
                event_source_identifier.Object,
                second_event_source_version,
                CausedBy.Unknown,
                DateTimeOffset.UtcNow
            );
            var second_event_and_envelope = new Letter(second_event_envelope, second_event.Object);
            var second_event_and_version = new VersionedEvent(second_event.Object, second_event_source_version);

            uncommitted_event_stream = new UncommittedEventStream(event_source.Object);
            uncommitted_event_stream.Append(first_event.Object, first_event_source_version);
            uncommitted_event_stream.Append(second_event.Object, second_event_source_version);

            event_envelopes.Setup(e => e.CreateFrom(event_source.Object, uncommitted_event_stream.VersionedEvents)).Returns(new IEnvelope[]
            {
                first_event_envelope,
                second_event_envelope
            });

            event_store.Setup(e => e.Commit(uncommitted_events));

            event_store.Setup(e => e.Commit(Moq.It.IsAny<IEnumerable<Letter>>())).Callback(
                (IEnumerable<Letter> e)=>
                {
                    uncommitted_events = e;
                    sequence_string = sequence_string + "1";
                });

            var numbers = new [] { first_event_sequence_number, second_event_sequence_number };
            var numbers_for_types = new [] { first_event_sequence_number_for_type, second_event_sequence_number_for_type };
            var sequence = 0;
            var sequence_for_types = 0;
            event_sequence_numbers.Setup(e => e.Next()).Returns(()=> numbers[sequence++]);
            event_sequence_numbers.Setup(e => e.NextForType(Moq.It.IsAny<IApplicationArtifactIdentifier>())).Returns(()=> numbers_for_types[sequence_for_types++]);

            committed_event_stream_sender.Setup(e => e.Send(Moq.It.IsAny<CommittedEventStream>()))
                .Callback((CommittedEventStream c)=>
                {
                    committed_event_stream = c;
                    sequence_string = sequence_string + "2";
                });
        };

        Because of = ()=> coordinator.Commit(transaction_correlation_id, uncommitted_event_stream);

        It should_commit_insert_event_with_correct_event_to_event_store = ()=> uncommitted_events.First().Contents.ShouldEqual(first_event.Object);

        It should_commit_two_events = ()=> uncommitted_events.Count().ShouldEqual(2);
        It should_hold_the_correct_correlation_id_for_first_event_when_committing = ()=> uncommitted_events.ToArray()[0].Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_sequence_number_for_first_event_when_committing = ()=> uncommitted_events.ToArray()[0].Envelope.SequenceNumber.ShouldEqual(first_event_sequence_number);
        It should_hold_the_correct_event_for_first_event_when_committing = ()=> uncommitted_events.ToArray()[0].Contents.ShouldEqual(first_event.Object);

        It should_hold_the_correct_correlation_id_for_second_event_when_committing = ()=> uncommitted_events.ToArray()[1].Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_sequence_number_for_second_event_when_committing = ()=> uncommitted_events.ToArray()[1].Envelope.SequenceNumber.ShouldEqual(second_event_sequence_number);
        It should_hold_the_correct_event_for_second_event_when_committing = ()=> uncommitted_events.ToArray()[1].Contents.ShouldEqual(second_event.Object);

        It should_send_two_events = ()=> committed_event_stream.Count.ShouldEqual(2);
        It should_hold_the_correct_correlation_id_for_first_event_when_sending = ()=> committed_event_stream.ToArray()[0].Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_sequence_number_for_first_event_when_sending = ()=> committed_event_stream.ToArray()[0].Envelope.SequenceNumber.ShouldEqual(first_event_sequence_number);
        It should_hold_the_correct_event_for_first_event_when_sending = ()=> committed_event_stream.ToArray()[0].Contents.ShouldEqual(first_event.Object);

        It should_hold_the_correct_correlation_id_for_second_event_when_sending = ()=> committed_event_stream.ToArray()[1].Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_sequence_number_for_second_event_when_sending = ()=> committed_event_stream.ToArray()[1].Envelope.SequenceNumber.ShouldEqual(second_event_sequence_number);
        It should_hold_the_correct_event_for_second_event_when_sending = ()=> committed_event_stream.ToArray()[1].Contents.ShouldEqual(second_event.Object);

        It should_commit_before_sending = ()=> sequence_string.ShouldEqual("12");
    }
}