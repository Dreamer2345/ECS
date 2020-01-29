using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.entityHandle
{
    public class ECSEntityHandler
    {
        List<ECSEntityHandle> entitys = new List<ECSEntityHandle>();
        static Random rndgen = new Random();

        public List<ECSEntityHandle> GetEntitys()
        {
            return entitys;
        }

        public ECSEntityHandle GetEntityFromIndex(int Index)
        {
            if ((Index > 0) && (Index < entitys.Count))
                return entitys[Index];
            else
                return null;
        }

        public ECSEntityHandle GetEntity(ulong ID)
        {
            return entitys.Find(x => x.ID == ID);
        }

        public void RemoveEntity(ulong ID)
        {
            ECSEntityHandle entity = entitys.Find(x => x.ID == ID);
            entity.Disposed = true;
            entitys.Remove(entity);
        }

        public bool HasKey(ulong ID)
        {
            return entitys.Find(x => x.ID == ID) != null;
        }

        public ECSEntityHandle GetNewEntity()
        {
            ulong NewID = 0;
            bool Found = false;
            while (!Found)
            {
                NewID = (ulong)((rndgen.Next() << 32) + rndgen.Next());
                Found = !HasKey(NewID);
            }

            ECSEntityHandle newEntity = new ECSEntityHandle(NewID);
            entitys.Add(newEntity);
            return newEntity;
        }
    }
}
