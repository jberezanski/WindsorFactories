using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class FooService : IFooService, IDisposable
    {
        private readonly IFooFactory fooFactory;

        public FooService(IFooFactory fooFactory)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got fooFactory #" + fooFactory.GetHashCode()));
            this.fooFactory = fooFactory;
        }

        public void Snarf()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Snarf, obtaining fooA from factory #" + fooFactory.GetHashCode()));
            var foo = this.fooFactory.GetFooA();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Snarf, obtained fooA #" + foo.GetHashCode() + " from factory #" + fooFactory.GetHashCode()));
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Snarf, releasing fooA #" + foo.GetHashCode() + " to factory #" + fooFactory.GetHashCode()));
            //this.fooFactory.Release(foo);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Snarf, released fooA #" + foo.GetHashCode() + " to factory #" + fooFactory.GetHashCode()));
        }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));

            if (Controls.DisposeFactories && this.fooFactory is IDisposable)
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, disposing fooFactory #" + fooFactory.GetHashCode()));
                ((IDisposable)this.fooFactory).Dispose();
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, fooFactory disposed #" + fooFactory.GetHashCode()));
            }
            else
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose, ------ not disposing fooFactory #" + fooFactory.GetHashCode()));
            }

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose end"));
        }
    }
}
