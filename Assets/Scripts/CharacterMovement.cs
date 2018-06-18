using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 8f;
	public float sprintSpeed = 10f;
	public float jumpSpeed = 15f;
	public float gravity = 20f;
	private Vector3 moveDirection = Vector3.zero;
	public bool isJumping = false;
	public bool isAttacking = false;
	public bool isClimbing = false;

	public GameObject character;
	public GameObject sword;

	// UI Objects
	public GameObject pauseMenu;

	[HideInInspector]public bool isFacingLeft = false;

	public bool isMoving = false;
	public bool isSprinting = false;
	public double previousX;
	public double currentX;

	// Object Shoot
	public bool hasArrows;
	public GameObject spawnRight;
	public GameObject spawnLeft;
	public GameObject arrow;

	// Bubble Shield
	public bool bubbleAbility=false;
	public int bubbleHits = 1;
	public GameObject bubbleShield;
	public float bubbleLifeSpan= 3f;
	
	// Lightning
	public bool lightningAbility=false;
	public int arc = 0;
	public GameObject lightning;

	/// SOUNDS
	private AudioSource source;
	public AudioClip swingSword;
	public AudioClip fireball;
	public AudioClip lightningSound;


	// Use this for initialization
	void Start () 
	{
		source = GetComponent<AudioSource>();
		sword.SetActive (false);
		lightning.gameObject.SetActive(false);
		bubbleShield.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		previousX = System.Math.Round(this.gameObject.transform.position.x, 2);
		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			isSprinting = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			isSprinting = false;
		}
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}

		// How Objects are thrown
		if (Input.GetKeyDown(KeyCode.Mouse1) && hasArrows) 
		{
			if (isFacingLeft==false) 
			{
				Instantiate (arrow, spawnRight.transform.position, transform.rotation);
				hasArrows = false;
				StartCoroutine(fireballDelay());
			}
			else if (isFacingLeft==true) 
			{
				Instantiate (arrow, spawnLeft.transform.position, transform.rotation);
				hasArrows = false;
				StartCoroutine(fireballDelay());
			}
			source.PlayOneShot(fireball);
		}

		// Lightning
		if (Input.GetKeyDown(KeyCode.E) && lightningAbility) 
		{
			lightning.gameObject.SetActive(true);
			lightningAbility = false;
			StartCoroutine(lightningDelay());
			source.PlayOneShot(lightningSound);
		}

		// Bubble Shield
		if (Input.GetKeyDown(KeyCode.Q) && bubbleAbility) 
		{
			bubbleShield.gameObject.SetActive(true);
			bubbleAbility = false;
			StartCoroutine(shieldLifeDelay(bubbleLifeSpan));
		}

		// Sword
		if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !isMoving) 
		{
			AnimateCharacter ac = gameObject.GetComponent<AnimateCharacter>();
			ac.curSpriteCount = 0;
			StartCoroutine(Attack(.4f));
			source.PlayOneShot(swingSword);
		}


		CharacterController controller = GetComponent<CharacterController>();


		if (isClimbing) 
		{
			moveDirection.x = Input.GetAxis ("Horizontal");
			moveDirection.y = Input.GetAxis ("Vertical");
			moveDirection *= speed;
			DBMovement();
		}
		else if (controller.isGrounded) 
		{
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			isJumping = false;
			DBMovement();

			if(isSprinting)
			{
				moveDirection *= sprintSpeed;
			}
			if(!isSprinting)
			{
				moveDirection *= speed;
			}
			
			if (Input.GetButton("Jump") && !isJumping)
			{
				moveDirection.y = jumpSpeed;
				isJumping = true;
			}
			moveDirection.y -= gravity * Time.deltaTime;
		}
		else 
		{
			moveDirection.x = Input.GetAxis ("Horizontal");
			if(isSprinting)
			{
				moveDirection.x *= sprintSpeed;
			}
			if(!isSprinting)
			{
				moveDirection.x *= speed;
			}
			DBMovement();
			moveDirection.y -= gravity * Time.deltaTime;
		}



		controller.Move(moveDirection * Time.deltaTime);
		currentX = System.Math.Round(this.gameObject.transform.position.x, 2);

		//Check to see if we are currently moving... this is needed for animation purposes...
		if(previousX != currentX)
		{
			isMoving = true;
		}
		else if(previousX == currentX)
		{
			isMoving = false;
		}
	}

	void DBMovement()
	{
		if(moveDirection.x > 0)
		{
			//Debug.Log ("I moved right!");
			isFacingLeft = false;
			character.transform.rotation = Quaternion.Euler (270, 180, 0);
		}
		else if(moveDirection.x < 0) 
		{
			//Debug.Log ("I moved left!");
			isFacingLeft = true;
			character.transform.rotation = Quaternion.Euler (270, 0, 0);
		}
	}
	IEnumerator fireballDelay()
	{
		yield return new WaitForSeconds(1f);
		hasArrows = true;
	}
	IEnumerator lightningDelay()
	{
		yield return new WaitForSeconds(.5f);
		lightning.SetActive (false);
		yield return new WaitForSeconds(1.5f);
		lightningAbility = true;
	}
	IEnumerator shieldLifeDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		bubbleShield.SetActive (false);
		yield return new WaitForSeconds(seconds);
		bubbleAbility = true;
	}
	IEnumerator Attack(float seconds)
	{
		sword.SetActive (true);
		isAttacking = true;
		yield return new WaitForSeconds(seconds);
		sword.SetActive (false);
		isAttacking = false;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag=="Ladder") 
		{
			isClimbing = true;
			isJumping = false;
		}
	}
	 void OnTriggerExit(Collider other)
	{
		if (other.tag=="Ladder") 
		{
			isClimbing = false;
		}
	}
}
