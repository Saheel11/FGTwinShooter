using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashScripts : MonoBehaviour
{
    CharacterController characterController;
    int go;

    [SerializeField]private float dashFloat = 100;
    float dashTimer;
    bool imDashing;
    private Vector2 rightStickPosition;

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


        if (imDashing)
        {
            characterController.Move(transform.forward * dashFloat * Time.deltaTime);
          //  imDashing = false;
          dashTimer -=Time.deltaTime;  
            if(dashTimer < 0)
            {

                imDashing = false;

            }

        }


        
    }
    void OnDash()
    {

       

       
        imDashing = true;
        dashTimer = 0.5f;
        // go = 1;


        // transform.Translate(Vector3.forward * 3000 * Time.deltaTime); 




    }
    private void OnLook(InputValue lookValue)
    {
        rightStickPosition = lookValue.Get<Vector2>();

        if (rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }


}
