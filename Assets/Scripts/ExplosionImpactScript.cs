using UnityEngine;
using System.Collections;

public class ExplosionImpactScript : MonoBehaviour 
{
	public float radius = 20.0f;
	public float power = 30.0f;

	// Use this for initialization
	void Start () 
	{
		//Explosion occured
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) 
		{
			if (hit && hit.GetComponent<Rigidbody>())
				hit.GetComponent<Rigidbody>().AddExplosionForce (power, explosionPos, radius, 3.0F);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
