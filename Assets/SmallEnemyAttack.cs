using UnityEngine;
using System.Collections;

public class SmallEnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks=0.5f;
	public int attackDamage=2;
	
	Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	SmallEnemyHealth smallEnemyHealth;
	bool playerInRange;
	float timer;
	float range=0;
	float chaseRange=10;
	
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
		smallEnemyHealth = GetComponent<SmallEnemyHealth> ();
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
			anim.SetBool("SmallIsIdle",true);
			anim.SetBool ("SmallIsAttacking",false);
		}
	}
	void Update ()
	{
		
		range = Vector3.Distance (transform.position, target.position);
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange && smallEnemyHealth.currentHealth > 0) 
		{
			Attack();
			anim.SetBool("SmallIsAttacking",true);
			anim.SetBool ("SmallIsIdle",false);
		}
		if (playerHealth.health <= 0) 
		{
			//anim.SetTrigger("PlayerDead");
			anim.SetBool ("SmallIsIdle",true);
			anim.SetBool("SmallIsAttacking",false);
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
			anim.SetBool("SmallIsIdle",true);
		}
		if (target != null && range <= chaseRange) {
			//		nextTime = Time.time + timeRate;				
			
			agent.destination = target.position;
			transform.LookAt (target);
			Idle = false;
			anim.SetBool ("SmallIsWalking", true);
		}
		if (range <= 10) {
			anim.SetBool ("SmallIsWalking", false);
			anim.SetBool ("SmallIsAttackng", true);
		}
		
	}
}
