using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

	public Sprite graphic;

	public string Name="";
	public string description = "";
	public int strengthBoost=0;
	public int magicBoost=0;
	public int speedBoost=0;
	public int criticalBoost=0;
	public int armorBoost=0;
	public int healthRestore = 0;

	public Stats st;
	public Inventory inv;

	public AudioSource source;
	public AudioClip equip;

	// Use this for initialization
	void Start () 
	{
		st = GameObject.FindGameObjectWithTag ("Player").GetComponent<Stats> ();
		inv = GameObject.Find("GameManager").GetComponent<Inventory> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();
		gameObject.name = Name;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Selected()
	{
		//inv.Name.text = Name;
		//inv.description.text = description;
	}
}
