using Machine.Specifications;

namespace Dolittle.Runtime.Events.Specs.for_Envelope
{
    public class when_building_with_new_sequence_number : given.an_envelope
    {
        static SequenceNumber new_sequence_number;
        static IEnvelope result;

        Establish context = () => new_sequence_number = 42L;

        Because of = () => result = envelope.WithSequenceNumber(new_sequence_number);

        It should_be_a_different_instance = () => result.GetHashCode().ShouldNotEqual(envelope.GetHashCode());
        It should_hold_the_new_sequence_number = () => result.SequenceNumber.ShouldEqual(new_sequence_number);
    }
}