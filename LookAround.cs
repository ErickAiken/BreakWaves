using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour{

    public Transform playerBody;
    float xRotation = 0f;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }//end Start

    void Update(){
        float delta = GameData.MOUSE_SENSITIVITY * 500.0f;
        float mouseX = Input.GetAxis("Mouse X")*delta*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*delta*Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }//end Update

}//end LookAround
