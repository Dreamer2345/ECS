using ECS.Core.GrowList;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.entityHandle
{
    public class ECSEntityHandler
    {
        GrowList<ECSEntityHandle> entitys = new GrowList<ECSEntityHandle>(1000,1000);

        public int EntityCount()
        {
            return entitys.Count;
        }

        public GrowList<ECSEntityHandle> GetEntitys()
        {
            return entitys;
        }

        public ECSEntityHandle GetEntityFromIndex(int Index)
        {
            return entitys[Index];
        }

        public ECSEntityHandle GetEntity(int ID)
        {
            return entitys.Find(x => x.ID == ID);
        }

        public void RemoveEntity(int ID)
        {
            ECSEntityHandle Entity = entitys.Find(x => x.ID == ID);
            Entity.Disposed = true;
            entitys.Remove(Entity);
        }

        public void RemoveEntity(ECSEntityHandle Entity)
        {
            Entity.Disposed = true;
            entitys.Remove(Entity);
        }

        public bool HasKey(int ID)
        {
            return entitys.Has(x => x.ID == ID);
        }

        public ECSEntityHandle GetNewEntity()
        {
            ECSEntityHandle newEntity = new ECSEntityHandle(0);
            int EntityID = entitys.Add(newEntity);
            newEntity.SetID(EntityID);
            return newEntity;
        }
    }
}
