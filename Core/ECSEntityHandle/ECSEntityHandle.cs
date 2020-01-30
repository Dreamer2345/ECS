using ECS.Core.BaseECSComponent;
using ECS.Core.GrowList;
using System;
using System.Collections.Generic;

namespace ECS.Core.entityHandle
{
    public class ECSEntityHandle
    {
        public bool Disposed = false;
        public ulong ID { get; private set; }
        public GrowList<Type> Components = new GrowList<Type>();

        public ECSEntityHandle(ulong entityID)
        {
            ID = entityID;
        }

        public bool HasComponent(object type)
        {
            return Components.Contains(type.GetType());
        }

        public override string ToString()
        {
            string ComponentString = "Components:\n";
            
            foreach(Type i in Components)
            {
                ComponentString += i.Name + "\n";
            }

            return "ID:" + ID + "\n" + ComponentString;
        }
    }
}
