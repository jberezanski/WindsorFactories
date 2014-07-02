using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public interface IFooFactory //: IDisposable
    {
        IFoo GetFooA();

        IFoo GetFooB();

        IFoo GetFooC();

        IFoo GetFooD();

        void Release(IFoo foo);
    }
}
