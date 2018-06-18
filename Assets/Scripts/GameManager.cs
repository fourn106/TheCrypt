using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

	public GameObject winScreen;
	private GameObject player;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	

	public void TriggerGameOver()
	{
		Application.LoadLevel (0);
	}

	public void WinLevel()
	{
		//Show Lose Screen
		winScreen.SetActive(true);
		StartCoroutine (EndGame ());

	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(10f);
		Application.LoadLevel (0);
	}


}
