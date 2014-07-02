using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class FooC : IFoo, IDisposable
    {
        private readonly IGizmoService gizmoService;

        public FooC(IGizmoService gizmoService)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got gizmoService #" + gizmoService.GetHashCode()));
            this.gizmoService = gizmoService;
        }

        public int DisposeCount { get; private set; }

        public void Frobulate()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frobulate(), calling Frob() on gizmoService #" + this.gizmoService.GetHashCode()));
            this.gizmoService.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frobulate() end"));
        }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));
            this.DisposeCount++;
        }
    }
}
