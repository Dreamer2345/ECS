using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.Interfaces
{
    interface IUpdatable : IBaseSystem
    {
        void Update(float Delta);
    }
}
