using UnityEngine;
using System.Collections;

public class smallProximity : MonoBehaviour {
	private GameObject player;
	private PlayerHealth playerhealth;
	public int smallhealth;
	private float range;
	Animator anim;
	// Use this for initialization
	void Start () {
		smallhealth = 100;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerhealth = player.GetComponent<PlayerHealth> ();
		range = 10;
		anim = GetComponent<Animator> ();
		
		anim.SetBool ("SmallIsAttacking",false);
	}

	void OnTriggerEnter (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			if (playerhealth.health>5f)
				playerhealth.health -= 5f;
			else
				playerhealth.health = 0f;
			anim.SetBool("SmallIsAttacking",true);
		}
		
	}
	void OnTriggerExit (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			
			anim.SetBool("SmallIsAttacking",false);
		}
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
