using UnityEngine;

namespace DefaultNamespace.Utils
{
    public static class GameObjectExtension
    {
        public static void Deactivate(this GameObject gameObject)
        {
            gameObject?.SetActive(false);
        }

        public static void Activate(this GameObject gameObject)
        {
            gameObject?.SetActive(true);
        }
    }
}