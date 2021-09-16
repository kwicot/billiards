using UnityEngine;

namespace DefaultNamespace.Utils
{
    public static class TransformExtension
    {
        public static void Deactivate(this Transform transform)
        {
            transform?.gameObject.Deactivate();
        }

        public static void Activate(this Transform transform)
        {
            transform?.gameObject.Activate();
        }
    }
}