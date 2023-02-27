using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CollectionsAndLinq
{
    internal class Timer : IDisposable
    {
        public bool IsDisposed { get; private set; }
        private static System.Timers.Timer timer;
        public event ElapsedEventHandler Elapsed;

        public List<int> Ids = new List<int>();


        public async Task<List<int>> MarkRandomTaskWithDelay(int ms)
        {
            timer = new System.Timers.Timer(ms);

            timer.Elapsed += Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            return Ids;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (disposing)
                    {
                        timer.AutoReset = false;
                        timer.Enabled = false;
                    }
                }
            }
            finally
            {
                IsDisposed = true;
            }

        }
    }
}
