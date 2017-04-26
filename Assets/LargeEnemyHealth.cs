﻿using UnityEngine;
using System.Collections;

public class LargeEnemyHealth : MonoBehaviour {

	
	public int startingHealth=100;
	public int currentHealth;
	public float sinkSpeed=2.5f;
	public int scoreValue=5;
	
	Animator anim;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking; 
	
	void Awake()
	{
		anim = GetComponent<Animator> ();
		hitParticles = GetComponentInChildren<ParticleSystem> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		currentHealth = startingHealth;
		
	}
	
	void Update ()
	{
		if (isSinking) 
		{
			transform.Translate(-Vector3.up*sinkSpeed*Time.deltaTime);
		}
	}
	
	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;
		currentHealth -= amount;
		hitParticles.transform.position = hitPoint;
		hitParticles.Play();
		if (currentHealth <= 0) 
		{
			Death();
		}
		
	}
	
	void Death()
	{
		isDead = true;
		capsuleCollider.isTrigger = true;
		anim.SetTrigger ("LargeDies");
	}
	public void StartSinking()
	{
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
		Destroy (gameObject,2f);
		
	}
}
