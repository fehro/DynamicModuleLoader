using System;
using System.Collections.Generic;
using DynamicModuleLoader.Common.Delegates;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal class EventEmitter<T>
    {
        #region Global Variables / Properties

        public event Event<T> Listeners;

        #endregion

        #region Public Methods

        /// <summary>
        /// Register a listener to listen for emitted events.
        /// </summary>
        public void RegisterListener(Event<T> listener)
        {
            Listeners += listener;
        }

        /// <summary>
        /// Emit an event to any registered listeners.
        /// </summary>
        public void Emit(T paramater)
        {
            //If there are no assigned listeners then just return.
            if (Listeners == null) return;

            Listeners.BeginInvoke(paramater, null, null);
        }

        #endregion
    }
}
