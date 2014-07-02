using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public sealed class FooConsumer : IFooConsumer, IDisposable
    {
        private readonly IFooFactory fooFactory;

        public FooConsumer(IFooFactory fooFactory)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got fooFactory #" + fooFactory.GetHashCode()));
            this.fooFactory = fooFactory;
        }

        public void Frob()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, obtaining FooA from factory #" + fooFactory.GetHashCode()));
            var foo = this.fooFactory.GetFooA();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, FooA #" + foo.GetHashCode() + " obtained from factory #" + fooFactory.GetHashCode()));
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulating FooA #" + foo.GetHashCode()));
            foo.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulated FooA #" + foo.GetHashCode()));
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, obtaining FooB from factory #" + fooFactory.GetHashCode()));
            var foo2 = this.fooFactory.GetFooB();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, FooB #" + foo2.GetHashCode() + " obtained from factory #" + fooFactory.GetHashCode()));
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulating FooB #" + foo2.GetHashCode()));
            foo2.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulated FooB #" + foo2.GetHashCode()));
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, obtaining FooC from factory #" + fooFactory.GetHashCode()));
            var foo3 = this.fooFactory.GetFooC();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, FooC #" + foo3.GetHashCode() + " obtained from factory #" + fooFactory.GetHashCode()));
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulating FooC #" + foo3.GetHashCode()));
            foo3.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frob, frobulated FooC #" + foo3.GetHashCode()));
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
