using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private void Start()
        {
            rigidbody = TryGetComponent(out Rigidbody rb) ? rb : gameObject.AddComponent<Rigidbody>();
            
            
        }


        public void Hit(CueEventHandler handler)
        {
            Vector3 direct = handler.cueForward * handler.strange;
            Debug.Log($"Hit with {handler.strange} strange");
            rigidbody.AddForce(direct);
        }
    }
}