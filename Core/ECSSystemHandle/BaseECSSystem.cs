using System;
using System.Collections.Generic;
using System.Text;


namespace ECS.Core.ECSSystemHandle
{
    public abstract class BaseECSSystem
    {
        public abstract void Update(float Delta);
    }
}
