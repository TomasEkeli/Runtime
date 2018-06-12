using Machine.Specifications;

namespace Dolittle.Runtime.Events.Specs.for_EventSourceVersion
{
    [Subject(typeof (EventSourceVersion))]
    public class when_getting_the_next_commit: given.a_range_of_event_source_versions
    {
        static EventSourceVersion starting_version;
        static EventSourceVersion next_version;

        Establish context = () => starting_version = new EventSourceVersion(1,1);

        Because of = () =>  next_version = starting_version.NextCommit();

        It should_increment_the_commit_version_by_one = () => next_version.Commit.ShouldEqual(starting_version.Commit + 1);
        It should_reset_the_sequence_number_to_zero = () => next_version.Sequence.ShouldEqual(0);
    }
}