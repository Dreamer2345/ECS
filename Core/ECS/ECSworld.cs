using ECS.Core.BaseECSComponent;
using ECS.Core.ECSSystemHandle;
using ECS.Core.entityHandle;
using System;

namespace ECS.Core.ECS
{
    public class ECSworld
    {
        ECSComponentPoolHandler poolHandler = new ECSComponentPoolHandler();
        ECSEntityHandler entityHandler = new ECSEntityHandler();
        ECSSystemHandler systemHandler = new ECSSystemHandler();

        public ECSEntityHandle GetNewEntity()
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

        public void AddComponent(object component, ECSEntityHandle entity)
        {
            if (entityHandler.HasKey(entity.ID))
                if(!entity.HasComponent(component))
                    poolHandler.AddComponent(component, entity);
                else
                    throw new Exception("Cannot add component from entity: Entity already has component of type: "+ component.GetType().Name);
            else
                throw new Exception("Cannot add component from entity: Invalid Entity ID");
        }

        public void RemoveComponent(Type type,ECSEntityHandle entity) 
        {
            if (entityHandler.HasKey(entity.ID))
                if(entity.HasComponent(type))
                    poolHandler.RemoveComponent(type,entity);
                else
                    throw new Exception("Cannot remove component from entity: Entity does not have component of type: " + type.Name);
            else
                throw new Exception("Cannot remove component from entity: Invalid Entity ID");
        }

        public object GetComponent(Type type, ECSEntityHandle entity)
        {
            if (entityHandler.HasKey(entity.ID))
            {
                if (entity.HasComponent(type))
                {
                    return poolHandler.GetPool(type).GetFromEntity(entity);
                }
                else
                    throw new Exception("Entity does not have that component type:"+ type.Name);
            }
            throw new Exception("Cannot Find component from entity: Invalid Entity ID");

        }

        public ECSEntityHandle GetEntityByIndex(int index)
        {
            return entityHandler.GetEntityFromIndex(index);
        }

        public override string ToString()
        {
            string str = "";
            foreach (ECSEntityHandle i in entityHandler.GetEntitys())
                str += i.ToString();

            return str;
        }
    }
}
