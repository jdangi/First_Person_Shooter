using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{
  //  public GUIText blcs;
   // public GUIText ScoreText;
	

    public float speed = 10f;

	CapsuleShoot cap;
	shoot sh;
	public int range=1000;
	//Score
	PlayerScore pscore;
	PlayerScore Blcs;

    //weapon
	PlayerHealth pwp;
    public int LiquidBomb = 100;
    public int Bomb = 100;

    // capsules
    public GameObject shotc;
    public GameObject shotc2;
    public GameObject shotc3;
    public GameObject shotc4;
    //Upgrade Variable
	public int cup = 0;

    //bombs
	parabolic pb;
    public GameObject shotb;
    public GameObject shotb2;
    public GameObject shotb3;
    public GameObject shotb4;
    //Upgrade Variable
    int bup = 0;

    //lqbombs
    public GameObject shotlb;
    public GameObject shotlb2;
    public GameObject shotlb3;
    public GameObject shotlb4;
    //Upgrade Variable
    int lbup = 0;

    public Transform shotSpawn;
    public Transform shotSpawnb;
    public Transform shotSpawnLq;
	public Transform sp;
    public float fireRate;

    private float nextFire;

    //parabolic ray
    public Texture2D cursorTexture;
    private RaycastHit hit;
    public static RaycastHit floorHit;
    Ray ray;

    //Weapon Switch
    public int weaponSelected = 0;

    void Update()
    {
       // Bombs.text = "Bombs:" + Bomb;
     //   LQBombs.text = "LQBombs:" + LiquidBomb;
	
        //   PlayerDead();
        Upgrade();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switch (weaponSelected)
            {
                case 0:
                    weaponSelected = 1;
                    break;

                case 1:
                    weaponSelected = 2;
                    break;

                case 2:
                    weaponSelected = 0;
                    break;

            }
        }


            switch (weaponSelected)
            {
				case 0:
					if(Input.GetButton("Fire1") && Time.time > nextFire)
					{
						nextFire = Time.time + fireRate;
						sh.Shoot(cup);
					}
				
				break;

                case 1:
                
                        if (Input.GetButton("Fire1") && Time.time > nextFire)
                        {
                            if (pwp.bomb > 0)
                            {
                                nextFire = Time.time + fireRate;				
								
                                    if (bup == 0 )
                                        Instantiate(shotb, shotSpawnb.position, shotSpawnb.rotation);
                                    else if (bup == 1)
                                        Instantiate(shotb2, shotSpawnb.position, shotSpawnb.rotation);
                                    else if (bup == 2)
                                        Instantiate(shotb3, shotSpawnb.position, shotSpawnb.rotation);
                                    else if (bup == 3)
                                        Instantiate(shotb4, shotSpawnb.position, shotSpawnb.rotation);
                               
								pwp.bomb--;
								
                            }
                        
                        }
                    break;
                case 2:
                    if (Input.GetButton("Fire1") && Time.time > nextFire)
                    {
                        if (pwp.ExpBomb > 0)
                        {
                            nextFire = Time.time + fireRate;

                            if (lbup == 0)
                                Instantiate(shotlb, shotSpawnLq.position, shotSpawnLq.rotation);
                            if (lbup == 1)
                                Instantiate(shotlb2, shotSpawnLq.position, shotSpawnLq.rotation);
                            if (lbup == 2)
                                Instantiate(shotlb3, shotSpawnLq.position, shotSpawnLq.rotation);
                            if (lbup == 3)
                                Instantiate(shotlb4, shotSpawnLq.position, shotSpawnLq.rotation);

                            pwp.ExpBomb--;
                         
                        }
                    }
                    break;

            }// end of switch	
    //    } //End of if(physics.raycast) Original
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Liquid Bomb"))
        {
            pwp.ExpBomb++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("bomb"))
        {
            pwp.bomb++;
            other.gameObject.SetActive(false);
        }

    }


    // Use this for initialization
    void Start()
    {
        //	health = 100;
        cup = 0;
        bup = 0;
        lbup = 0;
	
		GameObject gm = GameObject.FindWithTag ("Player");
		if(gm!=null)
		{
			Blcs =  gm.GetComponent<PlayerScore>();
			cap = gm.GetComponent<CapsuleShoot>();
		}
		GameObject go = GameObject.FindWithTag ("shootPoint");
		if(go!=null)
		sh= go.GetComponent<shoot>();

		GameObject wp = GameObject.FindWithTag ("Player");
		if (wp != null)
		pwp = wp.GetComponent<PlayerHealth> ();
    }

    

    // Weapons Upgrade
    void Upgrade()
    {
        //Capsule Upgrade
        if (Input.GetKeyDown(KeyCode.C) && cup <= 3 && Blcs.totalBloodCells >= 1000)
        {
            Blcs.totalBloodCells -= 1000;
    
      //      blcs.text = "BLCs:" + Blcs.totalBloodCells;
            cup++;
        }
        else if (Input.GetKeyDown(KeyCode.M) && cup == 3 && Blcs.totalBloodCells >= 1000)
        {
            cup = 3;
        }

        //Bomb Upgrade
        if (Input.GetKeyDown(KeyCode.B) && bup <= 3 && Blcs.totalBloodCells >= 2000)
        {
            Blcs.totalBloodCells -= 2000;
           
        //    blcs.text = "BLCs:" + Blcs.totalBloodCells;
            bup++;
        }
        else if (Input.GetKeyDown(KeyCode.M) && bup == 3 && Blcs.totalBloodCells >= 2000)
        {
            bup = 3;
           
        }

        //LiquidBomb Upgrade
        if (Input.GetKeyDown(KeyCode.L) && lbup <= 3 && Blcs.totalBloodCells >= 4000)
        {
            Blcs.totalBloodCells -= 4000;
           
          //  blcs.text = "BLCs:" + Blcs.totalBloodCells;
            lbup++;
        }
        else if (Input.GetKeyDown(KeyCode.M) && lbup == 3 && Blcs.totalBloodCells >= 4000)
        {
            lbup = 3;
        }
    }


}


