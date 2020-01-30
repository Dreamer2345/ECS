using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Core.GrowList
{
    class GrowListElement<T>
    {
        public T Data;
        bool Active = false;

        public bool IsActive { get { return Active; }  set { Active = value; } }

        public GrowListElement(T Data)
        {
            this.Data = Data;
        }
    }

    class GrowList<T>
    {
        const uint BaseCapSize = 100;
        uint CurrentSize;
        uint ArrayAllocAmount { get; set; } = 100;
        List<uint> FreeValues = new List<uint>();
        GrowListElement<T>[] Data;

        public void Clear()
        {
            for (uint i = 0; i < Data.Length; i++)
                Data[i].IsActive = false;
            FreeIndexes(0, CurrentSize);
        }
        public T GetValue(uint Index)
        {
            if ((Index >= 0) && (Index < CurrentSize))
            {
                if (Data[Index].IsActive)
                {
                    return Data[Index].Data;
                }
                else
                {
                    return default;
                }
            }
            return default;
        }
        public void Recycle(uint Index)
        {
            if((Index >= 0)&&(Index < CurrentSize))
            {
                if (Data[Index].IsActive)
                {
                    Data[Index].IsActive = false;
                    FreeIndex(Index);
                }
            }
        }
        void FreeIndex(uint Value, bool Overide = false)
        {
            if (Overide || (!Data[Value].IsActive && !FreeValues.Contains(Value)))
                FreeValues.Add(Value);
        }
        void FreeIndexes(uint StartVal, uint EndVal, bool Overide = false)
        {
            for (uint i = StartVal; i < EndVal; i++)
                FreeIndex(i, Overide);
        }
        public void GrowArray(uint NumberOfElements)
        {
            uint NewSize = NumberOfElements + CurrentSize;
            Array.Resize(ref Data, (int)NewSize);
            FreeIndexes(CurrentSize, NewSize, true);
            CurrentSize = NewSize;
        }
        public void Add(T Object)
        {
            if (FreeValues.Count == 0)
                GrowArray(ArrayAllocAmount);
            uint NewIndex = FreeValues[0];
            Data[NewIndex] = new GrowListElement<T>(Object) { IsActive = true };
            FreeValues.RemoveAt(0);
        }
        public GrowList()
        {
            Data = new GrowListElement<T>[BaseCapSize];
            CurrentSize = (uint)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        public GrowList(uint ArrayCapSize)
        {
            Data = new GrowListElement<T>[ArrayCapSize];
            CurrentSize = (uint)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        public GrowList(uint ArrayCapSize, uint ArrayMemAllocSize)
        {
            ArrayAllocAmount = ArrayMemAllocSize;
            Data = new GrowListElement<T>[ArrayCapSize];
            CurrentSize = (uint)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        
    }
}
