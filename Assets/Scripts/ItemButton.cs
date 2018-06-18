using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemButton : MonoBehaviour {

	public Inventory inv;
	public Item it;
	public int index=-1;
	public Button thisBT;
	public Button equip;
	public Text description;
	public Stats st;

	// Use this for initialization
	void Start () 
	{
		thisBT = this.gameObject.GetComponent<Button>();
		inv = GameObject.Find("GameManager").GetComponent<Inventory> ();
		index = System.Array.IndexOf (inv.itemSlots, thisBT);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void slotClicked()
	{
		it = inv.items[index].GetComponent<Item>();
		description.text = it.description;
		equip.GetComponent<Equip> ().it = it;
	}
}
