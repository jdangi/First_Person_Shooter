using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScript : MonoBehaviour 
{

	public void StartGame()
	{
		Application.LoadLevel("Scene1");
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
