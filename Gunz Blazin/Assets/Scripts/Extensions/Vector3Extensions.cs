using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Calculates normalized direction from one vector towards another
        /// </summary>
        /// <param name="from">the from vector</param>
        /// <param name="to">the to vector</param>
        /// <returns></returns>
        public static Vector3 DirectionTowardsNormalized(this Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        /// <summary>
        /// Returns true if two vectors's components are approximatly the same, false if not
        /// </summary>
        /// <param name="a">the first vector to compare</param>
        /// <param name="b">the second vector to compare</param>
        /// <param name="epsilon">the allowed difference between each of the vectors' components</param>
        /// <returns></returns>
        public static bool IsVectorComparableTo(Vector3 a, Vector3 b, float epsilon)
        {
            bool isXComparable = Mathf.Abs(a.x - b.x) <= epsilon;
            bool isYComparable = Mathf.Abs(a.y - b.y) <= epsilon;
            bool isZComparable = Mathf.Abs(a.z - b.z) <= epsilon;
            return isXComparable && isYComparable && isZComparable;
        }
    }
}