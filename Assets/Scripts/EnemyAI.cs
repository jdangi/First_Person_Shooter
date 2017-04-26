using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyAI : MonoBehaviour {

	NavMeshAgent agent;
	smallenemy charmove;

	public GameObject waypointholder;
	public List<Transform> waypoints = new List<Transform>();
	float targetTolerance=1;
	int waypointIndex;
	Vector3 targetPos;

	Animator anim;
	SwitchWeapons switchweapons;

	float patrolTimer;
	public float waitingTime=2;
	public float attackRate=4;
	float attackTimer;

	public List<GameObject> Enemies = new List<GameObject>();
	public GameObject EnemyToAttack;

	public enum AIstate
	{
		Patrol,Attack
	}
	public AIstate aiState;

	void Start () 
	{
		agent = GetComponentInChildren<NavMeshAgent>();
		charmove = GetComponent<smallenemy> ();;
		anim = GetComponent<Animator> ();
		switchweapons = GetComponent<SwitchWeapons> ();
		Transform[] wayp = waypointholder.GetComponentsInChildren<Transform> ();
		foreach (Transform tr in wayp)
		{
			if(tr != waypointholder.transform)
			{
				waypoints.Add(tr);
			}
		}

	}
	

	void Update () 
	{


		Patrolling ();
	}

 
	void Patrolling()
	{
		agent.speed = 20;
		if (waypoints.Count > 0) {
			if (agent.remainingDistance < agent.stoppingDistance) {
				patrolTimer += Time.deltaTime;
				if (patrolTimer >= waitingTime) {
					if (waypointIndex == waypoints.Count - 1) {
						waypointIndex = 0;
					} else {
						waypointIndex++;
					}

					patrolTimer = 0;
				}
			}
			else 
			{
				patrolTimer = 0;
			}
			agent.transform.position = transform.position;
			agent.destination = waypoints [waypointIndex].position;

			Vector3 velocity = agent.desiredVelocity * 0.5f;
			//charmove.CreateTarPoint();
		}
		else
		{
			agent.transform.position=transform.position;

			Vector3 lookPos = (waypoints.Count>0)? waypoints[waypointIndex].position : Vector3.zero;

		//	charmove.CreateTarPoint();
		}
	}
}
