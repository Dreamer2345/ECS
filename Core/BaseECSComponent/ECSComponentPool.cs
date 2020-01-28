using ECS.Core.entityHandle;
using System;
using System.Collections.Generic;

namespace ECS.Core.BaseECSComponent
{
    public class ECSComponentPool<T> where T : IBaseECSComponent
    {
        public Type ComponentPoolType = typeof(T);
        List<Tuple<T, EntityHandle>> Components = new List<Tuple<T, EntityHandle>>();

        public void AddToEntity(T Component, EntityHandle entity)
        {
            entity.Components.Add(typeof(T));
            Components.Add(Tuple.Create(Component, entity));
        }

        public void RemoveFromEntity(EntityHandle entity)
        {
            entity.Components.Remove(typeof(T));
            Components.Remove(Components.Find(x => x.Item2 == entity));
        }

        public T GetComponentFromEntity(EntityHandle entityHandle)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Item2 == entityHandle)
                {
                    return Components[i].Item1;
                }
            }

            return default;
        }

    }
}
