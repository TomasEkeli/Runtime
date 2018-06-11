﻿using System.Dynamic;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Runtime.Commands.Coordination.Specs.for_CommandContextManager
{
    [Subject(Subjects.establishing_context)]
    public class when_establishing_with_different_commands : given.a_command_context_manager
    {
        static ICommandContext firstCommandContext;
        static ICommandContext secondCommandContext;

        Because of = () =>
                         {
                             var firstCommand = new CommandRequest(CorrelationId.NotSet, Mock.Of<IApplicationArtifactIdentifier>(), new ExpandoObject());
                             var secondCommand = new CommandRequest(CorrelationId.NotSet, Mock.Of<IApplicationArtifactIdentifier>(), new ExpandoObject());
                             firstCommandContext = Manager.EstablishForCommand(firstCommand);
                             secondCommandContext = Manager.EstablishForCommand(secondCommand);
                         };
 

        It should_return_different_contexts = () => firstCommandContext.ShouldNotEqual(secondCommandContext);
    }
}