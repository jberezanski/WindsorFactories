﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindsorFactories
{
    public interface IGizmoFactory : IDisposable
    {
        IGizmo Create();

        void Release(IGizmo gizmo);
    }
}
