using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class FooD : IFoo, IDisposable
    {
        public FooD(IGizmo gizmo)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got gizmo #" + gizmo.GetHashCode()));
            this.Gizmo = gizmo;
        }

        public int DisposeCount { get; private set; }

        public IGizmo Gizmo { get; private set; }

        public void Frobulate()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frobulate"));
        }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));
            this.DisposeCount++;
        }
    }
}
