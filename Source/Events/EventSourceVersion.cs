﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;
using Dolittle.Concepts;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents the versioning for an event source
    /// </summary>
    public class EventSourceVersion : Value<EventSourceVersion>, IComparable<EventSourceVersion>
    {
        const float SEQUENCE_DIVISOR = 10000;

        /// <summary>
        /// Zero/null version
        /// </summary>
        public static readonly EventSourceVersion Zero = new EventSourceVersion(0,0);

        /// <summary>
        /// Creates an <see cref="EventSourceVersion"/> from a combined floating point
        /// </summary>
        /// <param name="combined"></param>
        /// <returns></returns>
        public static EventSourceVersion FromCombined(double combined)
        {
            var commit = (ulong)combined;
            var sequence = (uint)Math.Round(((combined - (double)commit) * SEQUENCE_DIVISOR));
            return new EventSourceVersion(commit, sequence);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersion"/>
        /// </summary>
        /// <param name="commit">Commit part of the version (major)</param>
        /// <param name="sequence">Sequence part of the version, within the commit (minor) </param>
        public EventSourceVersion(ulong commit, uint sequence) 
        {
            Commit = commit;
            Sequence = sequence;
        }

        /// <summary>
        /// Gets the commit number of the version
        /// </summary>
        public ulong Commit { get; set; }

        /// <summary>
        /// Gets the sequence number of the version
        /// </summary>
        public uint Sequence { get; set; }


        /// <summary>
        /// Increase the commit number and return a new version
        /// </summary>
        /// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion NextCommit()
        {
            var nextCommit = new EventSourceVersion(Commit + 1,0);
            return nextCommit;
        }

        /// <summary>
        /// Increase the sequence number and return a new version
        /// </summary>
        /// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion NextSequence()
        {
            var nextSequence = new EventSourceVersion(Commit,Sequence+1);
            return nextSequence;
        }


        /// <summary>
        /// Decrease the commit number and return a new version
        /// </summary>
        /// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion PreviousCommit()
        {
            var previousCommit = new EventSourceVersion(Commit - 1, 0);
            return previousCommit;
        }

        /// <summary>
        /// Returns an Initial version of the <see cref="IEventSource" /> with a Commit of 1 and a Sequence of 0
        /// </summary>
        /// <returns></returns>
        public static EventSourceVersion Initial()
        {
            return new EventSourceVersion(1, 0);
        }

        /// <summary>
        /// Compare this version with another version
        /// </summary>
        /// <param name="other">The other version to compare to</param>
        /// <returns>
        /// Less than zero - this instance is less than the other version
        /// Zero - this instance is equal to the other version
        /// Greater than zero - this instance is greater than the other version
        /// </returns>
        public int CompareTo(EventSourceVersion other)
        {
            var current = Combine();
            var otherVersion = other.Combine();
            return current.CompareTo(otherVersion);
        }

        /// <summary>
        /// Combines the Major / Minor number of Commit and Sequence into a single floating point number
        /// where the Commit is before the decimal place and Sequence is after.
        /// </summary>
        /// <returns></returns>
        public double Combine()
        {
            var majorNumber = (double) Commit;
            var minorNumber = ((double)Sequence / SEQUENCE_DIVISOR);
            var versionAsFloat = majorNumber + minorNumber;
            return versionAsFloat;
        }

#pragma warning disable 1591 // Xml Comments
        public override string ToString()
        {
            return string.Format("[ Version : {0}.{1} ]",Commit,Sequence);
        }
#pragma warning restore 1591 // Xml Comments
    }
}