using System;
using System.Collections;
using System.Collections.Generic;

namespace ECS.Core.GrowList
{
    public sealed class GrowListElement<T>
    {
        public T Data;
        bool Active = false;

        public bool IsActive { get { return Active; }  set { Active = value; } }

        public GrowListElement(T Data)
        {
            this.Data = Data;
        }
    }

    public class GrowList<T> : IEnumerable<T>
    {
        bool OverideData = false;
        const int BaseCapSize = 100;  
        int CurrentSize;
        int ArrayAllocIncrement { get; set; } = 100;
        List<int> FreeValues = new List<int>();
        List<int> TakenValues = new List<int>();
        GrowListElement<T>[] Data;
        
        public void Clear()
        {
            for (int i = 0; i < Data.Length; i++)
                Remove(i);
            FreeIndexes(0, CurrentSize);
            TakenValues.Clear();
        }
        public T GetValue(int Index)
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
        public void Recycle(int Index)
        {
            if((Index >= 0)&&(Index < CurrentSize))
            {
                if (Data[Index].IsActive)
                {
                    Remove(Index);
                    FreeIndex(Index);
                }
            }
        }
        private void FreeIndex(int Value, bool Overide = false)
        {
            if (Overide || (!Data[Value].IsActive && TakenValues.Contains(Value)))
            {
                FreeValues.Add(Value);
                TakenValues.Remove(Value);
            }
        }
        private void FreeIndexes(int StartVal, int EndVal, bool Overide = false)
        {
            for (int i = StartVal; i < EndVal; i++)
                FreeIndex(i, Overide);
        }
        private void GrowArray(int NumberOfElements)
        {
            ulong NewSize = (ulong)(NumberOfElements + CurrentSize);

            if (NewSize > int.MaxValue)
                NewSize = int.MaxValue;

            if(CurrentSize == (int)NewSize)
                return;

            Array.Resize(ref Data, (int)NewSize);
            FreeIndexes(CurrentSize, (int)NewSize, true);
            CurrentSize = (int)NewSize;
        }
        public void Add(T Object)
        {
            if (FreeValues.Count == 0)
                GrowArray(ArrayAllocIncrement);
            int NewIndex = FreeValues[0];
            Data[NewIndex] = new GrowListElement<T>(Object) { IsActive = true };
            FreeValues.RemoveAt(0);
            TakenValues.Add(NewIndex);
        }
        public void Remove(int Index)
        {
            Data[Index].IsActive = false;
            if(OverideData)
                Data[Index].Data = default;
            if (TakenValues.Contains(Index))
                FreeIndex(Index);
        }
        public bool Contains(T Element)
        {
            return Contains(Element, out int i);
        }
        public bool Contains(T Element, out int Index)
        {
            foreach (int i in TakenValues)
            {
                if (Element.Equals(Data[i].Data))
                {
                    Index = i;
                    return true;
                }
            }
            Index = 0;
            return false;
        }
        public T Find(Predicate<T> Where)
        {
            for (int i = 0; i < TakenValues.Count; i++)
                if (Where.Invoke(Data[TakenValues[(int)i]].Data))
                    return Data[TakenValues[(int)i]].Data;
            return default;
        }
        public int FindIndex(Predicate<T> Where)
        {
            for (int i = 0; i < TakenValues.Count; i++)
                if (Where.Invoke(Data[TakenValues[(int)i]].Data))
                    return i;
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public GrowList()
        {
            Data = new GrowListElement<T>[BaseCapSize];
            CurrentSize = (int)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        public GrowList(int ArrayCapSize)
        {
            Data = new GrowListElement<T>[ArrayCapSize];
            CurrentSize = (int)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        public GrowList(int ArrayCapSize, int ArrayMemAllocSize)
        {
            ArrayAllocIncrement = ArrayMemAllocSize;
            Data = new GrowListElement<T>[ArrayCapSize];
            CurrentSize = (int)Data.Length;
            FreeIndexes(0, CurrentSize, true);
        }
        ~GrowList()
        {
            FreeValues.Clear();
        }
    }

    public class GrowlistEnumerator<T> : IEnumerator
    {

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public GrowListElement<T> Current
        {
            get
            {
                return Elements[DataLocations[(int)CurrentIndex]];
            }
        }

        int CurrentIndex = 0;
        GrowListElement<T>[] Elements;
        List<int> DataLocations;

        public GrowlistEnumerator(GrowListElement<T>[] listElements, List<int> DataLocations)
        {
            Elements = listElements;
            this.DataLocations = DataLocations;
        }

        public bool MoveNext()
        {
            CurrentIndex++;
            return (CurrentIndex < DataLocations.Count);
        }

        public void Reset()
        {
            CurrentIndex = 0;
        }
    }

}
