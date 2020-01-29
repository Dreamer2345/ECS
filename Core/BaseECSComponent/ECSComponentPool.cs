using ECS.Core.entityHandle;
using System;
using System.Collections.Generic;

namespace ECS.Core.BaseECSComponent
{
    public sealed class ECSComponentPool
    {
        public static int PoolCount = 0;
        public Type ComponentType;
        List<Tuple<object, ECSEntityHandle>> Components = new List<Tuple<object, ECSEntityHandle>>();

        public ECSComponentPool(Type type)
        {
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
            entity.Components.Remove(ComponentType);
            Components.Remove(Components.Find(x => x.Item2 == entity));
        }
    }
}
