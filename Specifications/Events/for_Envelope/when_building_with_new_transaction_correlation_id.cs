using Dolittle.Runtime.Transactions;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Specs.for_Envelope
{
    public class when_building_with_new_transaction_correlation_id : given.an_envelope
    {
        static TransactionCorrelationId new_transaction_correlation_id;
        static IEnvelope result;

        Establish context = () => new_transaction_correlation_id = TransactionCorrelationId.New();

        Because of = () => result = envelope.WithTransactionCorrelationId(new_transaction_correlation_id);

        It should_be_a_different_instance = () => result.GetHashCode().ShouldNotEqual(envelope.GetHashCode());
        It should_hold_the_new_correlation_id = () => result.CorrelationId.ShouldEqual(new_transaction_correlation_id);
    }
}
