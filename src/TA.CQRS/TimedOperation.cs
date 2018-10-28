////namespace TA.CQRS
////{
////    using System;
////    using System.Diagnostics;

////    using Microsoft.Extensions.Logging;

////    internal class TimedOperation : IDisposable
////    {
////        private Stopwatch _stopwatch;
////        private string _operationName;
////        private ILogger _log;
////        private bool disposed;

////        public TimedOperation(string operationName)
////        {
////            this._operationName = operationName;
////            this._stopwatch = new Stopwatch();
////            this._stopwatch.Start();
////            this.OnStateChanged(TimedOperation.OperationState.Started);
////        }

////        public TimedOperation(string operationName, ILogger log)
////            : this(operationName)
////        {
////            this._log = log;
////        }

////        public TimedOperation.OperationState State { get; private set; }

////        public void SetComplete()
////        {
////            this._stopwatch.Stop();
////            this.OnStateChanged(TimedOperation.OperationState.Completed);
////        }

////        public void Dispose()
////        {
////            this.Dispose(true);
////            GC.SuppressFinalize((object)this);
////        }

////        protected virtual void OnStateChanged(TimedOperation.OperationState state)
////        {
////            this.State = state;
////            string message = this.BuildLogMessage(state);
////            if (string.IsNullOrWhiteSpace(message))
////                return;
////            if (this._log != null)
////                this._log.LogInformation(message, Array.Empty<object>());
////            else
////                Trace.TraceInformation(message);
////        }

////        protected virtual string BuildLogMessage(TimedOperation.OperationState state)
////        {
////            string str = string.Format("{0}: {1}", (object)this._operationName, (object)state.ToString().ToLower());
////            if (state.HasFlag((Enum)TimedOperation.OperationState.Stopped))
////            {
////                double totalSeconds = this._stopwatch.Elapsed.TotalSeconds;
////                if (totalSeconds > 0.0)
////                    str += string.Format(" (duration: {0} seconds)", (object)totalSeconds.ToString("N2"));
////            }
////            return str;
////        }

////        private void Dispose(bool disposing)
////        {
////            if (this.disposed)
////                return;
////            if (disposing && this._stopwatch.IsRunning)
////            {
////                this._stopwatch.Stop();
////                this.OnStateChanged(TimedOperation.OperationState.Failed);
////            }
////            this.disposed = true;
////        }

////        public enum OperationState
////        {
////            NotSet = 0,
////            Started = 1,
////            Stopped = 2,
////            Completed = 6,
////            Failed = 10, // 0x0000000A
////        }
////    }
////}