using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject skillTree;
	public GameObject inventory;
	public GameObject mainMenu;

	public Button mMBtn;
	public Button iBtn;
	public Button sTBtn;

	public SkillTree st;

	// Use this for initialization
	void Start () 
	{
		mMBtn.interactable = false;
		iBtn.interactable = true;
		sTBtn.interactable = true;
		st = GameObject.FindGameObjectWithTag ("SkillTree").GetComponent<SkillTree> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void mainMenuButton()
	{
		mainMenu.SetActive(true);
		skillTree.SetActive(false);
		inventory.SetActive(false);

		mMBtn.interactable = false;
		iBtn.interactable = true;
		sTBtn.interactable = true;
	}

	public void inventoryButton()
	{
		mainMenu.SetActive(false);
		skillTree.SetActive(false);
		inventory.SetActive(true);

		mMBtn.interactable = true;
		iBtn.interactable = false;
		sTBtn.interactable = true;
	}

	public void skillTreeButton()
	{
		mainMenu.SetActive(false);
		skillTree.SetActive(true);
		inventory.SetActive(false);

		mMBtn.interactable = true;
		iBtn.interactable = true;
		sTBtn.interactable = false;
		st.updateInteractibles ();
	}
}
