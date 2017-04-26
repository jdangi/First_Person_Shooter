using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject enemy;
	private Vector3 spawnPoint;
	bool spawnmastertank; 

	public GameObject KeyCup;
	void Start ()
	{
		spawnmastertank = false;

	

	}
	void Update()
	{
		spawnPoint =new Vector3(Random.Range(0.0F,500.0F), 0, Random.Range(0.0F, 500.0F));




		if (KeyCup.activeSelf == false&&spawnmastertank==false) 
		{
			Spawn();
			spawnmastertank = true;
		}
			

	
	
	}

	void Spawn ()
	{
		if(playerHealth.health <= 0f)
		{
			return;
		}
		
		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		Instantiate (enemy, spawnPoint,Quaternion.identity );
	}
}
