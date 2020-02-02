using ECS.Core.entityHandle;
using ECS.Core.GrowList;
using System;
using System.Collections.Generic;

namespace ECS.Core.BaseECSComponent
{
    public class ECSComponentPoolHandler
    {
        Dictionary<Type, int> valuePairs = new Dictionary<Type, int>();
        GrowList<object> EcsComponentPools = new GrowList<object>();

        public int RegisterNewComponentPool<T>() where T : IBaseECSComponent
        {
            if (valuePairs.ContainsKey(typeof(T)))
            {
                throw new Exception("ComponentType:" + typeof(T) + " Already Exists in component pools");
            }
            else
            {    
                int newID = EcsComponentPools.Add(new ECSComponentPool(typeof(T), this));
                valuePairs.Add(typeof(T), newID);
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

        public void AddComponent(object component, ECSEntityHandle entity)
        {
            Type componentType = component.GetType();
            if (valuePairs.ContainsKey(componentType))
            {
                
                int ComponentPoolID = valuePairs[component.GetType()];
                ((ECSComponentPool)EcsComponentPools[ComponentPoolID]).AddToEntity(component, entity);
            }
        }

        public void RemoveComponent(Type type,ECSEntityHandle entity)
        {
            if (valuePairs.ContainsKey(type))
            {

                int ComponentPoolID = valuePairs[type];
                ((ECSComponentPool)EcsComponentPools[ComponentPoolID]).RemoveFromEntity(entity);
            }
        }

        public bool HasPool(Type type)
        {
            return valuePairs.ContainsKey(type);
        }

        public int GetID(Type type)
        {
            if (HasPool(type))
            {
                return valuePairs[type];
            }
            return -1;
        }

        public ECSComponentPool GetPool(Type type)
        {
            if (HasPool(type))
            {
                throw new Exception("ComponentType:" + type + " Doesent Exist in component pools");
            }

            int PoolID = valuePairs[type];
            return (ECSComponentPool)EcsComponentPools[PoolID];
        }

        public int GetPoolCount()
        {
            return EcsComponentPools.Count;
        }

        public int GetComponentCount()
        {
            int Tally = 0;
            foreach(ECSComponentPool i in EcsComponentPools)
            {
                Tally += i.GetComponentCount();
            }
            return Tally;
        }

    }
}
