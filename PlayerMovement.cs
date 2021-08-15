using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpHeight = 7.5f;
    public GameObject terrain;
    private float groundDistance = 0.4f;
    private float walkSpeed = 5.0f;
    private float runSpeed = 10.0f;
    private float swimSpeed = 2.5f;
    private Vector3 spawnLocation;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start(){
      GetRandomSpawn();
    }//end Start

    // Update is called once per frame
    void Update(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }//

        GameData.CLOCK += Time.deltaTime;
        GameData.playerPosition = new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z);
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

    }//end Update

    void GetRandomSpawn(){
        Vector3[] mapLocations = terrain.GetComponent<MeshGenerator>().vertices;
        int max = mapLocations.Length;
        int n = Random.Range(0, max);
        spawnLocation = mapLocations[n];
        spawnLocation[1] += 5.0f;
        Vector3 newPosition = spawnLocation - transform.position;
        //controller.Move(newPosition);
    }//end GetRandomSpawn


}//end PlayerMovement
