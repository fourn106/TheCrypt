using UnityEngine;
using System.Collections;

public class objectThrow : MonoBehaviour {

	public CharacterMovement cm;

	public bool left;
	public GameObject throwable;

	// Use this for initialization
	void Start () 
	{
		cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement>();
		left = cm.isFacingLeft;
		throwable = null; // Find gameobject selected by player
		StartCoroutine(breakThrowable ());
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if (left) 
		{
			transform.Translate (Vector3.left * .15f);
		}
		else 
		{
			transform.Translate (Vector3.right * .15f);
		}
	}

	IEnumerator breakThrowable()
	{
		yield return new WaitForSeconds(2);
		Destroy (gameObject);
	}
}
