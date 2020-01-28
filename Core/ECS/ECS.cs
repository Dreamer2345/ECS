using ECS.Core.BaseECSComponent;
using ECS.Core.entityHandle;
using System;

namespace ECS.Core.ECS
{
    public class ECSworld
    {
        ECSComponentPoolHandler poolHandler = new ECSComponentPoolHandler();
        EntityHandler entityHandler = new EntityHandler();

        public EntityHandle GetNewEntity()
        {
            return entityHandler.GetNewEntity();
        }

        public void RemoveEntity(ulong ID)
        {

            entityHandler.RemoveEntity(ID);
        }

        public void RegisterNewComponentType<T>() where T : IBaseECSComponent
        {
            poolHandler.RegisterNewComponentPool<T>();
        }

        public void DeRegisterNewComponentType<T>() where T : IBaseECSComponent
        {
            poolHandler.DeRegisterComponentPool<T>();
        }

        public void AddComponent<T>(T component, EntityHandle entity) where T : IBaseECSComponent
        {
            if (entityHandler.HasKey(entity.ID))
                if(!entity.HasComponent<T>())
                    poolHandler.AddComponent<T>(component, entity);
                else
                    throw new Exception("Cannot add component from entity: Entity already has component of type: "+ typeof(T).Name);
            else
                throw new Exception("Cannot add component from entity: Invalid Entity ID");
        }

        public void RemoveComponent<T>(EntityHandle entity) where T : IBaseECSComponent
        {
            if (entityHandler.HasKey(entity.ID))
                if(entity.HasComponent<T>())
                    poolHandler.RemoveComponent<T>(entity);
                else
                    throw new Exception("Cannot remove component from entity: Entity does not have component of type: " + typeof(T).Name);
            else
                throw new Exception("Cannot remove component from entity: Invalid Entity ID");
        }

        public override string ToString()
        {
            string str = "";
            foreach (EntityHandle i in entityHandler.GetEntitys())
                str += i.ToString();

            return str;
        }

    }
}
