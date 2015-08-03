using System;
using System.Collections.Generic;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal class EventEmitter<TEventArgs>
    {
        #region Global Variables / Properties

        /// <summary>
        /// Registered listeners.
        /// </summary>
        private readonly List<EventHandler<TEventArgs>> _listeners;

        #endregion

        #region Constructor

        public EventEmitter()
        {
            _listeners = new List<EventHandler<TEventArgs>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Register a listener to listen for emitted events.
        /// </summary>
        public void RegisterListener(EventHandler<TEventArgs> listener)
        {
            _listeners.Add(listener);
        }

        /// <summary>
        /// Emit an event to any registered listeners.
        /// </summary>
        public void Emit(TEventArgs type)
        {
            _listeners.ForEach(x => 
                x.BeginInvoke(this, type, null, null)
            );
        }

        #endregion
    }
}
