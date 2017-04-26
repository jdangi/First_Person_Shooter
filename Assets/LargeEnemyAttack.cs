using UnityEngine;
using System.Collections;

public class LargeEnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks=0.5f;
	public int attackDamage=5;
	
	Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	LargeEnemyHealth largeEnemyHealth;
	bool playerInRange;
	float timer;
	float range=0;
	float chaseRange=15;
	
	public Transform target;
	NavMeshAgent agent ;
	
	void Start()
	{
		
		agent= GetComponent<NavMeshAgent>();
	}
	void Awake()
	{
		
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		largeEnemyHealth = GetComponent<LargeEnemyHealth> ();
		anim = GetComponent<Animator> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange=true;
			
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange=false;
		}
	}
	void Update ()
	{
		
		range = Vector3.Distance (transform.position, target.position);
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange && largeEnemyHealth.currentHealth > 0) 
		{
			Attack();
		}
		if (playerHealth.health <= 0) 
		{
			anim.SetTrigger("PlayerDead");
		}
		
	}
	void FixedUpdate()
	{
		Animating ();
	}
	void Attack()
	{
		timer = 0f;
		if (playerHealth.health > 0) 
		{
			playerHealth.Takedamage(attackDamage);
		}
	}
	public void Animating()
	{
		bool Idle =true;
		if (Idle == true) 
		{
			anim.SetBool("LargeIsIdle",true);
		}
		if (target != null && range <= chaseRange) {
			//		nextTime = Time.time + timeRate;				
			
			agent.destination = target.position;
			transform.LookAt (target);
			Idle = false;
			anim.SetBool ("LargeIsWalking", true);
		}
		if (range <= 15) {
			anim.SetBool ("LargeIsWalking", false);
			anim.SetBool ("LargeIsAttackng", true);
		}
		
	}
}
