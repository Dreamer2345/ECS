using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.Interfaces
{
    interface IInitializable : IBaseSystem
    {
        void Initialize();
    }
}
