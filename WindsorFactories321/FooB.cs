using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WindsorFactories
{
    public sealed class FooB : IFoo, IDisposable
    {
        private readonly IFooService fooService;

        public FooB(IFooService fooService)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), ".ctor, got fooService #" + fooService.GetHashCode()));
            this.fooService = fooService;
        }

        public int DisposeCount { get; private set; }

        public void Frobulate()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frobulate(), calling Snarf() on fooService #" + this.fooService.GetHashCode()));
            this.fooService.Snarf();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Frobulate() end"));
        }

        public void Dispose()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "Dispose"));
            this.DisposeCount++;
        }
    }
}
