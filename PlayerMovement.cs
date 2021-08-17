using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpHeight = 7.5f;
    private float groundDistance = 0.4f;
    private float walkSpeed = 5.0f;
    private float runSpeed = 10.0f;
    private float swimSpeed = 2.5f;
    private Vector3 spawnLocation;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isGrounded;

    void Update(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }//

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = transform.right*x + transform.forward*z;

        //If we are modifying run speed
        if(Input.GetKey(KeyCode.LeftShift)){
            controller.Move(dir * runSpeed * Time.deltaTime);
        }else{
            controller.Move(dir * walkSpeed * Time.deltaTime);
        }//

        //Handle jumping
        if(Input.GetKey(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }//

        //Handle gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        UpdateClock();
        
    }//end Update

    void UpdateClock(){
        GameData.CLOCK += Time.deltaTime;
    }//end UpdateClock

}//end PlayerMovement
