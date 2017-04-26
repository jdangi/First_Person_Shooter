using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

    int maxbullets = 12;
    int maxclips = 5;
    public int range = 1000;
    public float damage = 50f;
    public float fireRate = 0.05f;
    public int force = 200;
    Transform mypos = null;
	public GameObject cap1;
	public GameObject cap2;
	public GameObject cap3;
	public GameObject cap4;
	int speed=20;
	GameObject go;

	void Start () {
        mypos = this.transform;	
	}
	
	void Update () {
     
	}

    public void Shoot(int cup)
    {
		mypos = this.transform;	
        Vector3 direction = this.transform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(mypos.position, direction,out hit, range))
        {

			if(cup==0)
				 go =(GameObject)Instantiate(cap1,mypos.position,mypos.rotation);
			 if(cup==1)
				 go =(GameObject)Instantiate(cap2,mypos.position,mypos.rotation);
			 if(cup==2)
			   	go =(GameObject)Instantiate(cap3,mypos.position,mypos.rotation);
			 if(cup==3)
			   	go =(GameObject)Instantiate(cap4,mypos.position,mypos.rotation);

			go.transform.Rotate(Vector3.right * 90);
			
			Rigidbody rob= go.GetComponent<Rigidbody>();
			
			rob.velocity = this.transform.forward * speed ;
        }
    }
}
