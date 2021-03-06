﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Runtime.Commands;
using Dolittle.Validation;

namespace Dolittle.Runtime.Commands.Validation
{
    /// <summary>
    /// Represents the result of validation for a <see cref="CommandRequest"/>
    /// </summary>
    public class CommandValidationResult
    {
        /// <summary>
        /// Initializes an instance of <see cref="CommandValidationResult"/>
        /// </summary>
        public CommandValidationResult()
        {
            CommandErrorMessages = new string[0];
            ValidationResults = new ValidationResult[0];
        }

        /// <summary>
        /// Gets or sets the error messages related to a command, typically as a result of a failing ModelRule used from the validator
        /// </summary>
        public IEnumerable<string> CommandErrorMessages { get; set; }

        /// <summary>
        /// Gets or sets the validation results from any validator
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}
