using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public interface IFooConsumerFactory : IDisposable
    {
        IFooConsumer Create();

        void Release(IFooConsumer fooConsumer);
    }
}
