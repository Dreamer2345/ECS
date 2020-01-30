using ECS.Core.entityHandle;
using ECS.Core.GrowList;
using System;
using System.Collections.Generic;

namespace ECS.Core.BaseECSComponent
{
    public sealed class ECSComponentPool
    {
        ECSComponentPoolHandler Parent;
        public static int PoolCount = 0;
        public Type ComponentType;
        //Dictionary<ECSComponentPool, int> EntityToComponent = new Dictionary<ECSComponentPool, int>();
        GrowList<Tuple<object, ECSEntityHandle>> Components = new GrowList<Tuple<object, ECSEntityHandle>>();

        public ECSComponentPool(Type type,ECSComponentPoolHandler Parent)
        {
            if(Parent == null)
            {
                throw new NullReferenceException("Parent Pool Handler for type:" + type.Name + " Was Null");
            }
            this.Parent = Parent;
            ComponentType = type;
            PoolCount++;
        }

        ~ECSComponentPool()
        {
            PoolCount--;
        }

        public void AddToEntity(object Component, ECSEntityHandle entity)
        {
            entity.Components.Add(ComponentType);
            Components.Add(Tuple.Create(Component, entity));
        }

        public object GetFromEntity(ECSEntityHandle entity)
        {
            return Components.Find(x => x.Item2 == entity);
        }

        public void RemoveFromEntity(ECSEntityHandle entity)
        {
            entity.Components.Remove(Parent.GetID(ComponentType));
            int Index = Components.FindIndex(x => x.Item2 == entity); 
            Components.Recycle(Index);
        }
    }
}
