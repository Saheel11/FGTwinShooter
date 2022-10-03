using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public String whatIHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(whatIHit)) //Added if statement for it to destroy objects with name Destroyable in hierarchy
        {
            Debug.Log("Hit Enemy");
            Destroy(other.gameObject);
        }
        
        Debug.Log("Projectile destroys itself");
        Destroy(this.gameObject, 5f);
    }
}
