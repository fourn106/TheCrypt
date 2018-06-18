using UnityEngine;
using System.Collections;

public class EnemySword : MonoBehaviour {
	
	public GameObject sword;
	public AI ai;


	// Use this for initialization
	void Start () 
	{
		ai = this.transform.parent.gameObject.GetComponent<AI> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			StartCoroutine (Attack (1f));
		}
	}

	IEnumerator Attack(float seconds)
	{
		ai.isMoving = false;
		sword.SetActive (true);
		ai.isAttacking = true;
		yield return new WaitForSeconds(seconds);
		sword.SetActive (false);
		ai.isAttacking = false;
	}

}
