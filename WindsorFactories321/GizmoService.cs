using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class GizmoService : IGizmoService, IDisposable
    {
        private readonly IGizmoFactory gizmoFactory;

        public GizmoService(IGizmoFactory gizmoFactory)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got gizmoFactory #" + gizmoFactory.GetHashCode()));
            this.gizmoFactory = gizmoFactory;
        }

        public void Frob()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, obtaining gizmo from factory #" + gizmoFactory.GetHashCode()));
            var gizmo = this.gizmoFactory.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, obtained gizmo #" + gizmo.GetHashCode() + " from factory #" + gizmoFactory.GetHashCode()));
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, returning gizmo #" + gizmo.GetHashCode() + " to factory #" + gizmoFactory.GetHashCode()));
            //this.gizmoFactory.Release(gizmo);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, returned gizmo #" + gizmo.GetHashCode() + " to factory #" + gizmoFactory.GetHashCode()));
        }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));

            if (Controls.DisposeFactories)
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, disposing gizmoFactory #" + gizmoFactory.GetHashCode()));
                this.gizmoFactory.Dispose();
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, gizmoFactory disposed #" + gizmoFactory.GetHashCode()));
            }
            else
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, ------ not disposing gizmoFactory #" + gizmoFactory.GetHashCode()));
            }

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose end"));
        }
    }
}
