using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public static class Controls
    {
        static Controls()
        {
            DisposeFactories = true;
        }

        public static bool DisposeFactories { get; set; }
    }
}
