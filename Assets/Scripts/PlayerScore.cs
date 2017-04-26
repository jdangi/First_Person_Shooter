using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	public Text blcs;
	public Text ScoreText;
	public int Score ;
    public int totalBloodCells = 50000;

	void Start () {


		blcs.text = "BLCs:" + totalBloodCells;
		ScoreText.text = "Score:" + Score;
	}
	

	void Update () {
		blcs.text = "BLCs:" + totalBloodCells;
		ScoreText.text = "Score:" + Score;
	}
}
