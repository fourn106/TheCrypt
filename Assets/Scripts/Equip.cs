using UnityEngine;
using System.Collections;

public class Equip : MonoBehaviour {


	public Stats st;
	public Inventory inv;
	public AudioSource source;
	public AudioClip equip;
	public Item it;

	// Use this for initialization
	void Start () 
	{
		st = GameObject.FindGameObjectWithTag ("Player").GetComponent<Stats> ();
		source = GameObject.Find("Capsule").GetComponent<AudioSource>();
		inv = GameObject.Find("GameManager").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void EquipItem()
	{
		if (it.tag=="Weapon") 
		{
			if (inv.weapon!=null) 
			{
				st.strength -= inv.weapon.strengthBoost;
				st.magic -= inv.weapon.magicBoost;
				st.speed -= inv.weapon.speedBoost;
				st.critical -= inv.weapon.criticalBoost;
				st.armor -= inv.weapon.armorBoost;
				inv.items.Add(this.gameObject);
				inv.updateItemSlots();
			}
			st.strength += it.strengthBoost;
			st.magic += it.magicBoost;
			st.speed += it.speedBoost;
			st.critical += it.criticalBoost;
			st.armor += it.armorBoost;
			st.currentHealth += it.healthRestore;
			if (st.currentHealth >= st.health) 
			{
				st.currentHealth = st.health;
				inv.updateItemSlots ();
			}
			inv.items.Remove (this.gameObject);
			source.PlayOneShot (equip);
			inv.weapon = it;
		}
		else if (it.tag=="Armor") 
		{
			if (inv.weapon!=null) 
			{
				st.strength -= inv.weapon.strengthBoost;
				st.magic -= inv.weapon.magicBoost;
				st.speed -= inv.weapon.speedBoost;
				st.critical -= inv.weapon.criticalBoost;
				st.armor -= inv.weapon.armorBoost;
				inv.items.Add(this.gameObject);
				inv.updateItemSlots();
			}
			st.strength += it.strengthBoost;
			st.magic += it.magicBoost;
			st.speed += it.speedBoost;
			st.critical += it.criticalBoost;
			st.armor += it.armorBoost;
			st.currentHealth += it.healthRestore;
			if (st.currentHealth >= st.health) 
			{
				st.currentHealth = st.health;
				inv.updateItemSlots ();
			}
			inv.items.Remove (this.gameObject);
			source.PlayOneShot (equip);
			inv.armor = it;
		}
	}
}
