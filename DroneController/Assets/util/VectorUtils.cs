using UnityEngine;

namespace util
{

    public class VectorUtils
    {

        public static Vector3 ToVector3(Vector2 xy, float z)
        {
            return new Vector3(xy.x, xy.y, z);
        }

    }

}
