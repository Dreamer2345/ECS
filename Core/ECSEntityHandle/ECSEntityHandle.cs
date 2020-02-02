using ECS.Core.BaseECSComponent;
using ECS.Core.GrowList;
using System;
using System.Collections.Generic;

namespace ECS.Core.entityHandle
{
    public class ECSEntityHandle
    {
        public bool Disposed = false;
        public int ID { get; private set; }
        public GrowList<Tuple<Type, int>> Components = new GrowList<Tuple<Type, int>>();

        public void SetID(int ID)
        {
            this.ID = ID;
        }

        public ECSEntityHandle(int entityID)
        {
            ID = entityID;
        }

        public bool HasComponent(object type)
        {
            return Components.Has(x => x.Item1 == type.GetType());
        }

        public int GetComponentID(object type)
        {
            return Components.Find(x => x.Item1 == type.GetType()).Item2;
        }

        public override string ToString()
        {
            string ComponentString = "Components:\n";

            
            foreach(var i in Components)
            {
                ComponentString += "    Name:"+i.Item1.Name+" ID:"+i.Item2+ "\n";
            }

            return "ID:" + ID + "\n" + ComponentString;
        }
    }
}
