using UnityEngine;
using System.Collections;

public class HealthPowerUpScript : MonoBehaviour 
{
	private GameObject player;
	private PlayerDetails playerDetails;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerDetails = player.GetComponent<PlayerDetails> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate (new Vector3 (30,45,60) * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			GameObject.FindGameObjectWithTag("HealthPowerUp").SetActive(false);
			if (playerDetails.playerHealth < 100) 
			{
				playerDetails.playerHealth += 20;
			}
			if (playerDetails.playerHealth > 100)
			{
				playerDetails.playerHealth = 100;
			}
		}
	}
}
