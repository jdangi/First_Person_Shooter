using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

	public Camera viewPort;
	CharacterController cc;
	Vector3 moveVector;

	float xspeed;
	float zspeed;

	float vVeocity;
	public float gravity = 1f;
	public float moveSpeed;
	public float jumpSpeed;
	
	void Start () {
	//	Cursor.visible = false;
	//	Cursor.lockState = CursorLockMode.Locked;

		viewPort = GetComponentInChildren<Camera>();
		cc = GetComponent<CharacterController>();
	}

	void Update () {
	//	Cursor.visible = false;
	//	Cursor.lockState = CursorLockMode.Locked;

		xspeed = Input.GetAxis ("Horizontal");
		zspeed = Input.GetAxis ("Vertical");
	
		moveVector = new Vector3 (xspeed,0,zspeed);
		moveVector = transform.TransformDirection (moveVector) * moveSpeed;

		vVeocity += Physics.gravity.y * gravity * Time.deltaTime ;

		if(cc.isGrounded)
		{
			if(Input.GetButtonDown("Jump"))
			{
				vVeocity = jumpSpeed;
			}
		}
		moveVector.y = vVeocity;
		cc.Move (moveVector * Time.deltaTime);
	}
}
