using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Button resumeBtn;
	public Button saveBtn;
	public Button loadBtn;
	public Button quitBtn;

	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Resume()
	{
		pauseMenu.SetActive (false);
		Time.timeScale = 1f;
	}

	public void Quit()
	{
		Application.Quit();
	}
}
