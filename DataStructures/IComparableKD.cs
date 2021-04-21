namespace DataStructures
{
    public interface IComparableKD<T>
    {
        /// <summary>
        /// Compare 2 objects on k-level of tree.
        /// </summary>
        /// <param name="o"> Object to be compared with </param>
        /// <param name="k"> Dimension on which will compare </param>
        /// <returns></returns>
        int CompareDimensionKey(T o, int k);

        /// <summary>
        /// Check if keys of compared object are equal.
        /// </summary>
        /// <param name="o"> Object to be compared with </param>
        /// <returns></returns>
        bool KeysAreEqual(T o);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        bool IsBetween(T o1, T o2);
    }
}
