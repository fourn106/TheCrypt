using UnityEngine;
using System.Collections;

public class BubbleShield : MonoBehaviour {

	public CharacterMovement cm;
	public int health;

	public AudioSource source;
	public AudioClip bubbleShieldActive;

	// Use this for initialization
	void Start () 
	{
		cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.activeSelf) 
		{
			GameObject.FindGameObjectWithTag("Hitbox").GetComponent<BoxCollider>().enabled=false;
		}
	}

	public void CheckShieldLife()
	{
		if (health<=0)
		{
			this.gameObject.SetActive(false);
			health = cm.bubbleHits;
			StartCoroutine(HitBoxDelay());
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Weapon") 
		{
			health--;
			CheckShieldLife();
		}
	}

	IEnumerator HitBoxDelay()
	{
		yield return new WaitForSeconds(.5f);
		GameObject.FindGameObjectWithTag("Hitbox").GetComponent<BoxCollider>().enabled=true;
	}
}
