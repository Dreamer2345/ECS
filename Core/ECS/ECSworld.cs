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

        public void RemoveEntity(int ID)
        {
            RemoveEntity(entityHandler.GetEntity(ID));
            
        }

        public void RemoveEntity(ECSEntityHandle entity)
        {
            foreach (Tuple<Type, int> i in entity.Components) {
                poolHandler.RemoveComponent(i.Item1, entity);
            }
            entityHandler.RemoveEntity(entity);
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
            if (!entityHandler.HasKey(entity.ID))
                throw new Exception("Cannot add component from entity: Invalid Entity ID");
            if (entity.HasComponent(component))
                throw new Exception("Cannot add component from entity: Entity already has component of type: " + component.GetType().Name);



            poolHandler.AddComponent(component, entity);
        }

        public void RemoveComponent(Type type, ECSEntityHandle entity)
        {
            if (entityHandler.HasKey(entity.ID))
                throw new Exception("Cannot remove component from entity: Invalid Entity ID");

            if (!entity.HasComponent(type))
                throw new Exception("Cannot remove component from entity: Entity does not have component of type: " + type.Name);


            poolHandler.RemoveComponent(type, entity);
        }

        public object GetComponent(Type type, ECSEntityHandle entity)
        {
            if (!entityHandler.HasKey(entity.ID))
                throw new Exception("Cannot Find component from entity: Invalid Entity ID");

            if (!entity.HasComponent(type))
                throw new Exception("Entity does not have that component type:" + type.Name);

            return poolHandler.GetPool(type).GetFromEntity(entity);
        }

        public ECSEntityHandle GetEntityByIndex(int index)
        {
            return entityHandler.GetEntityFromIndex(index);
        }

        public void Update(float Delta)
        {
            //systemHandler
        }

        public override string ToString()
        {
            string str = "";
            


            str += "EntityCount:"+entityHandler.EntityCount() + "\n";
            str += "ComponentPoolCount:" + poolHandler.GetPoolCount() + "\n";
            str += "ComponentCount:" + poolHandler.GetComponentCount() + "\n";
            return str;
        }
    }
}
