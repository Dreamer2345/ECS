using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.Interfaces
{
    interface IDestroyable : IBaseSystem
    {
        void Destroy();
    }
}
