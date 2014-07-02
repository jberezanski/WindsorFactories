using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class Gizmo : IGizmo, IDisposable
    {
        public Gizmo()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor"));
        }

        public int DisposeCount { get; private set; }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));
            this.DisposeCount++;
        }
    }
}
