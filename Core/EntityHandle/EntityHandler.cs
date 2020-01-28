using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.entityHandle
{
    public class EntityHandler
    {
        List<EntityHandle> entitys = new List<EntityHandle>();
        static Random rndgen = new Random();

        public List<EntityHandle> GetEntitys()
        {
            return entitys;
        }

        public EntityHandle GetEntity(ulong ID)
        {
            return entitys.Find(x => x.ID == ID);
        }

        public void RemoveEntity(ulong ID)
        {
            EntityHandle entity = entitys.Find(x => x.ID == ID);
            entity.Disposed = true;
            entitys.Remove(entity);
        }

        public bool HasKey(ulong ID)
        {
            return entitys.Find(x => x.ID == ID) != null;
        }

        public EntityHandle GetNewEntity()
        {
            ulong NewID = 0;
            bool Found = false;
            while (!Found)
            {
                NewID = (ulong)((rndgen.Next() << 32) + rndgen.Next());
                Found = !HasKey(NewID);
            }

            EntityHandle newEntity = new EntityHandle(NewID);
            entitys.Add(newEntity);
            return newEntity;
        }
    }
}
