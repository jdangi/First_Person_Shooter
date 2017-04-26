using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class PauseScript : MonoBehaviour {

	GameObject PauseMenu;
	bool paused;
	bool muted;
	[SerializeField]
	Text mutetext;


	// Use this for initialization
	void Start () {
		paused = false;
			PauseMenu=GameObject.Find("PauseMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
		}
		if (paused) {
			PauseMenu.SetActive (true);
			Time.timeScale = 0;
		} else if (!paused) {
			PauseMenu.SetActive (false);
			Time.timeScale = 1;
		}
		if (muted) {
			AudioListener.volume = 0;
			mutetext.text = "unmute";
		} else if (!muted) {
			AudioListener.volume = 1;
	//		mutetext.text = "mute";
		}
			
	}	

		public void Resume()
		{
			paused=false;
		}

		public void MainMenu()
		{
		Application.LoadLevel ("mainmenu");
	}
		public void save()
		{
		PlayerPrefs.SetInt ("currenscenesave", Application.loadedLevel);
	}
		public void Load()
		{
		Application.LoadLevel (1);
		//Application.LoadLevel (PlayerPrefs.GetInt ("currentscenesave"));
	}
		 public void Mute()
		{
		muted = !muted;
	}
		public void Quit()
		{
		Application.Quit ();
	}
		

}
