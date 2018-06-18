using UnityEngine;
using System.Collections;

public class collectible : MonoBehaviour {

	public Inventory inv;
	public GameObject prefab;
	public AudioSource source;
	public AudioClip lootDrop;

	// Use this for initialization
	void Start () 
	{
		inv = GameObject.Find("GameManager").GetComponent<Inventory> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();
		source.PlayOneShot(lootDrop);
		StartCoroutine(breakItem (10));
		this.transform.rotation = Quaternion.Euler (90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator breakItem(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Player") 
		{
			if (inv.items.Count <20) 
			{
				inv.items.Add(prefab);
				this.gameObject.GetComponent<Renderer>().enabled=false;
				this.gameObject.GetComponent<BoxCollider>().enabled=false;
				StartCoroutine(breakItem (10));
				inv.updateItemSlots();
			}
		}
	}
}
