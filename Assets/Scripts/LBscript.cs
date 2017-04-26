using UnityEngine;
using System.Collections;

public class LBscript : MonoBehaviour {
	Rigidbody rb;
	public GameObject explosion;
	
	void Start () {
		rb= GetComponent<Rigidbody >();
		StartCoroutine(Delay());
	

	}
	IEnumerator Delay() {
	//	print (Time.time);
		yield return new WaitForSeconds (7);
	//	print (Time.time);

        float time = 0.1f;

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
			GetComponent<AudioSource>().Play ();
		}
        yield return new WaitForSeconds(time);
		Destroy (gameObject);
	}
}
