using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.attachedRigidbody)
        {
            //TODO Прикрутить пул менеджер
            Destroy(other.gameObject);
        }
    }
}
