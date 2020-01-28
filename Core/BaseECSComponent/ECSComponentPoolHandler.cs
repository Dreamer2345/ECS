using ECS.Core.entityHandle;
using System;
using System.Collections.Generic;

namespace ECS.Core.BaseECSComponent
{
    public class ECSComponentPoolHandler
    {
        Dictionary<Type, int> valuePairs = new Dictionary<Type, int>();
        List<object> EcsComponentPools = new List<object>();

        public int RegisterNewComponentPool<T>() where T : IBaseECSComponent
        {
            if (valuePairs.ContainsKey(typeof(T)))
            {
                throw new Exception("ComponentType:" + typeof(T) + " Already Exists in component pools");
            }
            else
            {
                int newID = EcsComponentPools.Count;
                valuePairs.Add(typeof(T), newID);
                EcsComponentPools.Add(new ECSComponentPool<T>());
                return newID;
            }
        }

        public void DeRegisterComponentPool<T>() where T : IBaseECSComponent
        {
            if (!valuePairs.ContainsKey(typeof(T)))
            {
                throw new Exception("ComponentType:" + typeof(T) + " Doesent Exist in component pools");
            }
            else
            {
                throw new Exception("DeRegistering of Component pools not implemented");
            }
        }

        public void AddComponent<T>(T component, EntityHandle entity) where T : IBaseECSComponent
        {
            Type componentType = component.GetType();
            if (valuePairs.ContainsKey(componentType))
            {

                int ComponentPoolID = valuePairs[component.GetType()];
                ((ECSComponentPool<T>)EcsComponentPools[ComponentPoolID]).AddToEntity(component, entity);
            }
        }

        public void RemoveComponent<T>(EntityHandle entity) where T : IBaseECSComponent
        {
            if (valuePairs.ContainsKey(typeof(T)))
            {

                int ComponentPoolID = valuePairs[typeof(T)];
                ((ECSComponentPool<T>)EcsComponentPools[ComponentPoolID]).RemoveFromEntity(entity);
            }
        }

        public bool HasPool(Type type)
        {
            return valuePairs.ContainsKey(type);
        }

        public ECSComponentPool<T> GetPool<T>() where T : IBaseECSComponent
        {
            if (HasPool(typeof(T)))
            {
                throw new Exception("ComponentType:" + typeof(T) + " Doesent Exist in component pools");
            }

            int PoolID = valuePairs[typeof(T)];
            return (ECSComponentPool<T>)EcsComponentPools[PoolID];
        }
    }
}
