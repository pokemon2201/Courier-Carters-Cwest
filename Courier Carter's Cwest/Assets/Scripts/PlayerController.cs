﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplyer;
    public float jumpForce;
    //public Rigidbody RB;
    public CharacterController charController;

    private Vector3 moveDirection;
	private Vector3 respawn;
    public float gravityScale;
	private bool dead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //RB = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }
	

    // Update is called once per frame
    void Update()
    {
        //RB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, RB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        /*if (Input.GetButtonDown("Jump"))
        {
            RB.velocity = new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift)) moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed * sprintMultiplyer) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        else moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = yStore;

        if (charController.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);
		
		if (dead) { 
		Debug.Log(transform.position);
		charController.enabled = false;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		charController.transform.position = respawn;
		charController.enabled = true;
		dead = false;
		Debug.Log("seen dead");
		Debug.Log(transform.position);
		}
    }
	
		private void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Checkpoint"){
			respawn = other.transform.position;
			Debug.Log(respawn);
		}
		if(other.tag == "Death"){
			dead = true;
			Debug.Log("touched death" + respawn);
		}
	}
}
