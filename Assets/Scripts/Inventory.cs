using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<GameObject> items;
	public Button[] itemSlots;
	public Item it;
	public Item itSelected;
	public Sprite blankSprite;
	public Item armor = null;
	public Item weapon = null;

	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void updateItemSlots()
	{
		for (int i = 0; i < 20; i++) 
		{
			if (i<items.Count) 
			{
				GameObject go =	(GameObject)items[i];
				it = go.GetComponent<Item>();
				itemSlots[i].GetComponent<Image>().sprite = it.graphic;
				itemSlots[i].interactable = true;
			}
			else 
			{
				itemSlots[i].GetComponent<Image>().sprite = blankSprite;
				itemSlots[i].interactable = false;
			}
		}
	}
}
