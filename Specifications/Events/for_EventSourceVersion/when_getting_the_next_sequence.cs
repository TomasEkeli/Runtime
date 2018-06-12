using Machine.Specifications;

namespace Dolittle.Runtime.Events.Specs.for_EventSourceVersion
{
    [Subject(typeof (EventSourceVersion))]
    public class when_getting_the_next_sequence: given.a_range_of_event_source_versions
    {
        static EventSourceVersion starting_version;
        static EventSourceVersion next_version;

        Establish context = () => starting_version = new EventSourceVersion(1,1);

        Because of = () =>  next_version = starting_version.NextSequence();

        It should_increment_the_sequence_number_by_one = () => next_version.Sequence.ShouldEqual(starting_version.Sequence + 1);
        It should_keep_the_commit_number_the_same = () => next_version.Commit.ShouldEqual(starting_version.Commit);
    }
}