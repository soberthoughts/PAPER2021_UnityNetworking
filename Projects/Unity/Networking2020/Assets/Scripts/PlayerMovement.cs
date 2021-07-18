using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;


public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    public float speed = 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsLocalPlayer)
        {
            MovePlayer();
            LookPlayer();
        }
        
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f); // normalizes it back to 1
        move = transform.TransformDirection(move);
        controller.SimpleMove(move * speed);
    }

    private void LookPlayer()
    {
        float mouseXinput = Input.GetAxis("Mouse X") * 3f;

        transform.Rotate(0, mouseXinput, 0);
    }

}
