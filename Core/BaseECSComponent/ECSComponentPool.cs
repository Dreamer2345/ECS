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
        GrowList<Tuple<object, ECSEntityHandle>> Components = new GrowList<Tuple<object, ECSEntityHandle>>(1000,1000);

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
            int Index = Components.Add(Tuple.Create(Component, entity));
            entity.Components.Add(new Tuple<Type, int>(ComponentType, Index));
        }

        public object GetFromEntity(ECSEntityHandle entity)
        {
            return Components.Find(x => x.Item2 == entity);
        }

        public void RemoveFromEntity(ECSEntityHandle entity)
        {
            Tuple<Type, int> componentIDPair = entity.Components.Find(x => x.Item1 == ComponentType);
            Components.Recycle(componentIDPair.Item2); 
        }

        public void RemoveFromEntityID(int ID)
        {
            int Index = Components.FindIndex(x => x.Item2.ID == ID);
            Components.Recycle(Index);
        }

        public int GetComponentCount()
        {
            return Components.Count;
        }
    }
}
