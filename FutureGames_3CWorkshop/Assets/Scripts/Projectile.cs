using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Destroyable") //Added if statement for it to destroy objects with name Destroyable in hierarchy
        {
            Debug.Log("Hit destroyable");
            Destroy(other.gameObject);
        }
        
        Debug.Log("Projectile destroys itself");
        Destroy(this.gameObject);
    }
}
