using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target;
	public float xOffset = 0f;
	public float yOffset = 0f;
	public float zOffset = -30f;
	CharacterController cc;

	// Use this for initialization
	void Start () 
	{
		//gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		ResetCamera();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
			//if the player is alive, run the function for the camera to follow the player
			UpdatePosition();
	}

	public void ResetCamera()
	{
		//reset camera to new character after player has died (assuming more lives are available)
		target = null;
		target = GameObject.FindGameObjectWithTag("Player");
	}

	void UpdatePosition()
	{
		//Check to make sure we have a target; if not we find the player
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player");
		}
		if(target != null)
		{
			this.transform.position = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, -50f);
		}
	}
}
