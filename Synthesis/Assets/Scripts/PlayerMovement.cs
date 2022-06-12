using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float LRMove = 0f;
    bool crouch = false, jump = false;
    // Update is called once per frame
    void Update()
    {
        LRMove = Input.GetAxisRaw("Horizontal") * runSpeed;
      //for input manager later on, but for now, i have the big stupid 
      // if (Input.GetButtonDown("Jump"))
      // {
      //     jump = true;
      // }
        if (Input.GetKeyDown("space"))
        {
             jump = true;
        }
 
      // if (Input.GetButtonDown("Crouch"))
      // {
      //     crouch = true;
      // }
      // else if (Input.GetButtonUp("Crouch"))
      // {
      //     crouch = false;
      // }
      // {
      //     crouch = true;
      // }
    }

    private void FixedUpdate()
    {
        controller.Move(LRMove * Time.fixedDeltaTime, crouch, jump);
         jump = false;
    }
}
