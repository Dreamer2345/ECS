using ECS.Core.ECS;
using ECS.Core.GrowList;
using ECS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.ECSSystemHandle
{
    public class ECSSystemHandler
    {
        ECSworld Parent;
        GrowList<BaseECSSystem> systems = new GrowList<BaseECSSystem>();

        public int AddSystem(BaseECSSystem system) 
        {
            return systems.Add(system);
        }


        public void Update(float Delta)
        {
            foreach(BaseECSSystem i in systems)
            {
                i.Update(Delta);
            }
        }
    }
}
