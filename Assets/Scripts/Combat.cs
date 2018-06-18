using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public int strongAttack;
	public int quickAttack;

	public Stats st;

	// Use this for initialization
	void Start () 
	{
		st = GameObject.FindGameObjectWithTag ("Player").GetComponent<Stats> ();
		strongAttack = st.strength * 2;
		quickAttack = st.strength;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
