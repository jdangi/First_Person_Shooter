using UnityEngine;
using System.Collections;

public class DoorOpenScript : MonoBehaviour 
{
	private PlayerDetails pd;
	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pd = player.GetComponent<PlayerDetails> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

/*	void OnTriggerEnter (Collider other)
	{
		if (pd.currentLevel > 1) 
		{
			if (other.tag.Equals ("Player")) 
			{
				GameObject.FindGameObjectWithTag("Level2Door").SetActive(false);
			}
		}
	}

 */ }
