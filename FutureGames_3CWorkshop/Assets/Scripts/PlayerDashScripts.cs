using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashScripts : MonoBehaviour
{
    CharacterController characterController;
    int go;

    public float dashFloat = 30;
    public float maxTimer;
    public float dashTimer;

    public LayerMask mask;
    public Collider[] colliders;

    [HideInInspector] public bool imDashing;
    [HideInInspector] public Vector2 rightStickPosition;

    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 30 * Time.deltaTime);
        //Debug.Log(go);

        Vector3 testDirection = new Vector3(rightStickPosition.x, 0, rightStickPosition.y);


        //if(Physics.SphereCast(this.transform.position, 5 , -transform.up, out hit, mask))
        //{

        //    if(hit.collider != null)
        //    {

        //        Debug.Log("I Hit " + hit.collider.name);

        //    }

        //}
        colliders = Physics.OverlapSphere(transform.position, 1, mask);
  






        if (imDashing)
        {
          characterController.Move(transform.forward * dashFloat * Time.deltaTime);
          //  imDashing = false;
          dashTimer -=Time.deltaTime; 
            
            if(dashTimer < 0)
            {

                imDashing = false;

            }
            foreach (var hitCollerer in colliders)
            {

                Debug.Log("iHit" + hitCollerer.gameObject.name);
                Destroy(hitCollerer.gameObject);

            }
        }


        
    }
    public void OnDash()
    {

       

       
        imDashing = true;
        dashTimer = maxTimer;
        // go = 1;


        // transform.Translate(Vector3.forward * 3000 * Time.deltaTime); 




    }
    public void OnLook(InputValue lookValue)
    {
        rightStickPosition = lookValue.Get<Vector2>();

        if (rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    



}
