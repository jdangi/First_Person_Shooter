using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

	public float speed;
	Rigidbody rb;

	void Start () {
		rb= GetComponent<Rigidbody >();
		rb.velocity = transform.forward * speed;
	}
}
