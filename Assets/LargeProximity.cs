using UnityEngine;
using System.Collections;

public class LargeProximity : MonoBehaviour {

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
		range = 20;
		anim = GetComponent<Animator> ();
		
		anim.SetBool ("LargeIsAttacking",false);
	}
	
	void OnTriggerEnter (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			if (playerhealth.health>10f)
				playerhealth.health -= 10f;
			else
				playerhealth.health = 0f;
			//playerhealth.health -= 10f;
			anim.SetBool("LargeIsAttacking",true);
		}
		
	}
	void OnTriggerExit (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			
			anim.SetBool("LargeIsAttacking",false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
