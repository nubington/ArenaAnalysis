using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arena_analysis
{
    struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 Zero
        {
            get
            {
                return new Vector3();
            }
        }
    }
}
