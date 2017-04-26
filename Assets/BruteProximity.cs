using UnityEngine;
using System.Collections;

public class BruteProximity : MonoBehaviour {

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
		range = 30;
		anim = GetComponent<Animator> ();
		
		anim.SetBool ("BruteIsAttacking",false);
	}
	
	void OnTriggerEnter (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			if (playerhealth.health>15f)
				playerhealth.health -= 15f;
			else
				playerhealth.health = 0f;
			//playerhealth.health -= 15f;
			anim.SetBool("BruteIsAttacking",true);
		}
		
	}
	void OnTriggerExit (Collider other)
	{
		
		if (other.tag.Equals ("Player")) 
		{
			
			anim.SetBool("BruteIsAttacking",false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
