﻿using System.Dynamic;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Runtime.Commands.Security.Specs.for_CommandSecurityManager
{
    public class when_authorizing_a_command : given.a_command_security_manager
    {
        static CommandRequest command;

        Establish context = () => 
        {
            var artifact = Artifact.New();
            command = new CommandRequest(CorrelationId.Empty, artifact.Id, artifact.Generation, new ExpandoObject());
        };

        Because of = () => command_security_manager.Authorize(command);

        It should_delegate_the_request_for_security_to_the_security_manager = () => security_manager_mock.Verify(s => s.Authorize<HandleCommand>(command), Moq.Times.Once());
    }
}
