﻿using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Events;
using Dolittle.Runtime.Events;

namespace Dolittle.Events.Specs.for_EventSource
{
    [Subject(Subjects.reapplying_events)]
    public class when_reapplying_a_stream_of_committed_events : given.a_stateful_event_source
    {
        static IEvent second_event;
        static Mock<IEnvelope> second_event_envelope;
        static IEvent third_event;
        static Mock<IEnvelope> third_event_envelope;
        static CommittedEventStream event_stream;

        Establish context =
            () =>
            {
                @event = new SimpleEvent();
                event_envelope = new Mock<IEnvelope>();
                event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero);

                second_event = new SimpleEvent();
                second_event_envelope = new Mock<IEnvelope>();
                second_event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero.NextSequence());

                third_event = new SimpleEvent();
                third_event_envelope = new Mock<IEnvelope>();
                third_event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero.NextCommit().NextSequence());

                event_stream = new CommittedEventStream(event_source_id,new[] {
                    new Letter(event_envelope.Object, @event),
                    new Letter(second_event_envelope.Object, second_event),
                    new Letter(third_event_envelope.Object, third_event),
                });
            };

        Because of = () => event_source.ReApply(event_stream);

        It should_not_add_the_events_to_the_uncommited_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
        It should_increment_the_commit_of_the_version = () => event_source.Version.Commit.ShouldEqual(2);
        It should_being_with_a_sequence_of_zero = () => event_source.Version.Sequence.ShouldEqual(0);
        It should_have_applied_the_event = () => event_source.EventApplied.ShouldBeTrue();
    }
}