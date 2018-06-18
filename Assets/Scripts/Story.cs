using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Story : MonoBehaviour {

	public GameObject title;
	public GameObject story;
	public GameObject end;
	//public GameObject[] story;
	public Button clicker;
	public int count = 0;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SwitchImage ());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ClickToContinue()
	{
		//story [count].SetActive (false);
		count++;
		if (count>=2) 
		{
			clicker.interactable = false;
		}
	}

	IEnumerator SwitchImage()
	{
		yield return new WaitForSeconds(2f);
		title.SetActive (false);
		yield return new WaitForSeconds(7f);
		story.SetActive (false);
	}
}
