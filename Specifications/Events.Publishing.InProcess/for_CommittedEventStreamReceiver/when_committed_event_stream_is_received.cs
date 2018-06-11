﻿using System;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Publishing.InProcess.Specs.for_CommittedEventStreamReceiver
{
    public class when_committed_event_stream_is_received : given.a_committed_event_stream_receiver
    {
        static CommittedEventStream committed_event_stream;
        static CommittedEventStream received_committed_event_stream;

        Establish context = () =>
        {
            committed_event_stream = new CommittedEventStream(Guid.NewGuid(), new Letter[0]);
            committed_event_stream_receiver.Received += c => received_committed_event_stream = c;
        };

        Because of = () => committed_event_stream_bridge.Raise(c => c.Received += null, committed_event_stream);

        It should_forward_the_received_event_stream = () => received_committed_event_stream.ShouldEqual(committed_event_stream);
    }
}
