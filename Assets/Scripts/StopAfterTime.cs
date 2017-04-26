using UnityEngine;
using System.Collections;

public class StopAfterTime : MonoBehaviour {

    public float time = 1.0f;
	
	void Start () {
        stopEmitting();
	}

    void stopEmitting()
    {
            gameObject.GetComponent<ParticleSystem>().Emit(2) ;
    }

}
