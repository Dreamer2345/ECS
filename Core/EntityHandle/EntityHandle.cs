using ECS.Core.BaseECSComponent;
using System;
using System.Collections.Generic;

namespace ECS.Core.entityHandle
{
    public class EntityHandle
    {
        public bool Disposed = false;
        public ulong ID { get; private set; }
        public List<Type> Components = new List<Type>();

        public EntityHandle(ulong entityID)
        {
            ID = entityID;
        }

        public bool HasComponent<T>() where T : IBaseECSComponent
        {
            return Components.Contains(typeof(T));
        }

        public override string ToString()
        {
            string ComponentString = "Components\n";
            
            foreach(Type i in Components)
            {
                ComponentString += i.Name + "\n";
            }

            return "ID:" + ID + "\n" + ComponentString;
        }
    }
}
