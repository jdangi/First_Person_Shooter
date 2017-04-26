using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public GameObject gun;
	public GameObject axe;
	public GameObject bow;
	public GameObject playerbody;
	public float health = 100f;
    public Text Playerhealth;
    public Text GameOver;
    PlayerWeapons pw;
	PlayerScore ps;



	//Weapons
	public int bomb=100;
	public int ExpBomb=100;
	public Text Bombs;
	public Text ExpBombs;

    void Update()
    {
        PlayerDead();
        Playerhealth.text = "Player Health:" + health;
		Bombs.text = "Bombs:" + bomb;
		ExpBombs.text = "Explosion Bombs:" + ExpBomb;
		if (Input.GetKeyDown (KeyCode.R))
			Restart ();

    }

    void Start()
    {
        health = 100;
        Playerhealth.text = "Player Health:" + health;
		Bombs.text = "Bombs:" + bomb;
		ExpBombs.text = "Explosion Bombs:" + ExpBomb;
        GameObject go = GameObject.FindWithTag("Player");
        if (go != null)
        {
            pw = go.GetComponent<PlayerWeapons>();
        }

		GameObject gm = GameObject.FindWithTag ("Player");
    	if(gm!=null)
			ps = gm.GetComponent<PlayerScore>();
	}

	public void Restart()
	{
		health = 100f;
		Playerhealth.text = "Player Health:" + health;
		GameOver.text = "";
		gun.SetActive (true);
		playerbody.SetActive (true);
		axe.SetActive (false);
		bow.SetActive (false);
		bomb = 100;
		ExpBomb = 100;
		ps.totalBloodCells = 50000;
		ps.Score = 0;

//		print ("bmb" + pw.Bomb);
		//pw.Bomb = 100;
		//pw.LiquidBomb = 100;



	}

    void PlayerDead()
    {
        if (health <= 3)
        {
            GameOver.text = "Game Over!!!";
            Playerhealth.text = "Player Health:" + health;
			gun.SetActive(false);
			playerbody.SetActive(false);
			//GameObject.FindWithTag("gun").SetActive(false);
            //GameObject.FindWithTag("playerbody").SetActive(false);

        }
    }
	public void Takedamage(int amount)
	{
		health = health - amount;
	}
    public void OnTriggerEnter(Collider other)
    {
      //  if (other.gameObject.CompareTag("enemybullet") && health>0)
     //   {
         //   health = health - 5;
      //      print("HL:" + health);
          //  Destroy(other.gameObject);
       // }


        if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            other.gameObject.SetActive(false);
            if (health < 100)
            {
                health += 20;
            }
            if (health > 100)
                health = 100;

        }
    }



}

















































































































