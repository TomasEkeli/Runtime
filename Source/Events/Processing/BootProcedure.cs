namespace Dolittle.Runtime.Events.Processing
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dolittle.Bootstrapping;
    using Dolittle.Collections;
    using Dolittle.Logging;
    using Dolittle.Runtime.Events;
    using Dolittle.Runtime.Events.Processing;
    using Dolittle.Runtime.Events.Store;
    using Dolittle.Runtime.Tenancy;
    using Dolittle.Types;

    /// <summary>
    /// Represents the <see cref="ICanPerformBootProcedure">boot procedure</see> for <see cref="EventProcessors"/>
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        IInstancesOf<IKnowAboutEventProcessors> _systemsThatKnowAboutEventProcessors;
        ITenants _tenants;

        IScopedEventProcessingHub _processingHub;
        ILogger _logger;

        public BootProcedure(IInstancesOf<IKnowAboutEventProcessors> systemsThatKnowAboutEventProcessors, ITenants tenants, IScopedEventProcessingHub processingHub, ILogger logger)
        {
            _processingHub = processingHub;
            _logger = logger;
            _tenants = tenants;
            _systemsThatKnowAboutEventProcessors = systemsThatKnowAboutEventProcessors;

        }

        /// <inheritdoc />
        public bool CanPerform() => true;

        /// <inheritdoc />
        public void Perform()
        {
            ProcessInParallel();
            _processingHub.BeginProcessingEvents();
        }

        void ProcessInParallel()
        {
            Parallel.ForEach(_systemsThatKnowAboutEventProcessors.ToList(), (system) =>
            {
                Parallel.ForEach(system.ToList(), (processor) =>
                {
                    Parallel.ForEach(_tenants.ToList(), (t) =>
                    {
                        _processingHub.Register(new ScopedEventProcessor(t, processor,_logger));
                    });
                });
            });
        }
    }
}