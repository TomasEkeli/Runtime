using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Events;
using System;

namespace Dolittle.Runtime.Events.Specs.for_CommittedEventStream
{
    public class when_creating_a_stream_with_one_event 
    {
        static IEvent @event;
        static CommittedEventStream event_stream;
        static Mock<IEnvelope> envelope;
        static EventSourceId event_source_id = Guid.NewGuid();

        Establish context = () =>
        {
            @event = new SimpleEvent();
            envelope = new Mock<IEnvelope>();
        };

        Because of = () => event_stream = new CommittedEventStream(event_source_id, new [] { new Letter(envelope.Object, @event) });

        It should_have_events = () => event_stream.HasEvents.ShouldBeTrue();
        It should_have_an_event_count_of_1 = () => event_stream.Count.ShouldEqual(1);
    }
}