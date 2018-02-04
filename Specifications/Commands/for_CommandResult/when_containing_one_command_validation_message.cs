﻿using System.Linq;
using Machine.Specifications;

namespace doLittle.Runtime.Commands.Specs.for_CommandResult
{
    public class when_containing_one_command_validation_message 
    {
        static CommandResult result;
        static string error_message = "Something went wrong";

        Because of = () => result = new CommandResult
        {
            CommandValidationMessages = new [] { error_message }
        };

        It should_not_be_valid = () => result.Invalid.ShouldBeTrue();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
        It should_have_only_the_command_validation_message_in_all_validation_errors = () =>
                                                                                          {
                                                                                              result.AllValidationMessages.Count().ShouldEqual(1);
                                                                                              result.AllValidationMessages.First().ShouldEqual(error_message);
                                                                                          };
    }
}
