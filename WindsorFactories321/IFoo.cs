﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorFactories
{
    public interface IFoo
    {
        int DisposeCount { get; }

        void Frobulate();
    }
}
