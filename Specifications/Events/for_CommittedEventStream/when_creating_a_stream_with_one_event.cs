﻿using Machine.Specifications;
using Moq;
using System;
using It = Machine.Specifications.It;
using Dolittle.Events;
using Dolittle.Artifacts;
using Dolittle.Runtime.Events.Store;
using Dolittle.Execution;
using Dolittle.Runtime.Events.Specs;
using specs = Dolittle.Runtime.Events.Specs.given;

namespace Dolittle.Runtime.Events.Specs.for_CommittedEventStream
{
    public class when_creating_a_stream_with_one_event : given.an_empty_committed_event_stream
    {
        static IEvent @event;
        static CommittedEvent committed_event;

        Establish context = () =>
        {
            @event = new specs.SimpleEvent();
            var metadata = new EventMetadata(EventId.New(),new VersionedEventSource(event_source_id,ArtifactId.New()),CorrelationId.New(),new Artifact(ArtifactId.New(),1),DateTime.UtcNow,specs.Events.an_original_context());
            committed_event = new CommittedEvent(new CommittedEventVersion(1,1,0),metadata,@event);
        };

        Because of = () => event_stream = new CommittedEvents(event_source_id, new [] { committed_event });

        It should_have_events = () => event_stream.HasEvents.ShouldBeTrue();
        It should_have_an_event_count_of_1 = () => event_stream.Count.ShouldEqual(1);
    }
}