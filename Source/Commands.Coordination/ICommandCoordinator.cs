﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Runtime.Commands.Coordination
{
    /// <summary>
    /// Defines a coordinator for coordinating commands coming into the system
    /// </summary>
    public partial interface ICommandCoordinator
    {
        /// <summary>
        /// Handle a command
        /// </summary>
        /// <param name="command"><see cref="CommandRequest">command</see> to handle</param>
        /// <returns>
        /// Result from the handle.
        /// Within the result one can check if the handling was success or not
        /// </returns>
        CommandResult Handle(CommandRequest command);
    }
}
