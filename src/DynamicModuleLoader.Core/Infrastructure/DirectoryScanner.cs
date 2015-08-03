using System;
using System.IO;
using System.Timers;

namespace DynamicModuleLoader.Core.Infrastructure
{
    internal class DirectoryScanner
    {
        #region Global Variables / Properties

        private Timer _timer;
        private const int ModuleScanFrequencySeconds = 5;
        private readonly string _modulesDirectory;
        private readonly string _moduleFilePattern;
        private readonly EventEmitter<FileInfo> _eventEmitter;

        #endregion

        #region Constructor

        public DirectoryScanner(string modulesDirectory, string moduleFilePattern)
        {
            _modulesDirectory = AppDomain.CurrentDomain.BaseDirectory + modulesDirectory;
            _moduleFilePattern = moduleFilePattern;

            //Setup the event emitter.
            _eventEmitter = new EventEmitter<FileInfo>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the directory scanner.
        /// </summary>
        public void Start()
        {
            //Check the directory scanner is not already running.
            if (_timer != null) throw new Exception("Directory scanner is already running");

            //Start the timer.
            _timer = new Timer(ModuleScanFrequencySeconds * 1000);
            _timer.Elapsed += Tick;
            _timer.Enabled = true;
            _timer.Start();
        }

        /// <summary>
        /// Stop the directory scanner.
        /// </summary>
        public void Stop()
        {
            //Check the directory scanner is running.
            if (_timer == null) return;

            //Stop the timer.
            _timer.Stop();
            _timer = null;
        }

        /// <summary>
        /// Register an on file found listener.
        /// </summary>
        public void RegisterOnFileFoundListener(EventHandler<FileInfo> listener)
        {
            _eventEmitter.RegisterListener(listener);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The timer tick event handler.
        /// </summary>
        private void Tick(object sender, ElapsedEventArgs e)
        {
            //Get all the file names in the modules directory.
            var filePaths = Directory.GetFiles(_modulesDirectory, _moduleFilePattern);

            foreach (var filePath in filePaths)
            {
                //Get the file info.
                var fileInfo = new FileInfo(filePath);

                //Emit an of file found event.
                _eventEmitter.Emit(fileInfo);
            }
        }

        #endregion
    }
}
