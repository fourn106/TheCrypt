using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public CharacterMovement cm;
	public GameObject lightningExtension;

	// Use this for initialization
	void Start () 
	{
		cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void chainLightning()
	{
		if (cm.arc>0) 
		{
			lightningExtension.SetActive (true);
			StartCoroutine(lightningDelay());
		}
	}
	IEnumerator lightningDelay()
	{
		yield return new WaitForSeconds(.5f);
		lightningExtension.SetActive (false);
	}
}
