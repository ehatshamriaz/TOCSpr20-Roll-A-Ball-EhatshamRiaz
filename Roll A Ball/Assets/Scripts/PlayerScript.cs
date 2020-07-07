using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using roll_a_ball_helper;

public class PlayerScript : MonoBehaviour {
    HelperClass HPclass = new HelperClass();
	
	public Transform player;
	public float speed;
	public GameObject RightExplosionPartical,WrongExplosionPartical;
	public Text cubeCountText;
	public Text score;
	public GameObject completeLevelUI;
	public AudioSource audioSource ,audioNo,audiospin,audioBridge;

	private Vector3 position;
	private Rigidbody rb;
	private int count ,palindromesCount;
	private bool canSpawnHere; 
	private bool isPalindrome;
	private int cubeCount ;
   
	void Start ()
    {
        
		rb = GetComponent<Rigidbody>(); 
		count = 0;
		cubeCount=10;
		palindromesCount=0;
		objectCount ();

	}
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}
	void Update () {
		GameObject[] thingyToFind = GameObject.FindGameObjectsWithTag ("PickUp");
		  cubeCount = thingyToFind.Length;
		cubeCountText.text="Cubes Left: "+cubeCount;
		objectCount();
	}
	void OnTriggerExit(Collider other) 
	{
		if(other.gameObject.CompareTag ("bridge"))
		{
			if (audioBridge.isPlaying)
			{
				audioBridge.Stop();
			}
		}
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{

			string str=other.transform.parent.gameObject.GetComponent<TextMesh>().text;
			other.transform.parent.gameObject.SetActive(false);
            if (HPclass.checkPalindrome(str))
			{
				audioSource.Play();
				isPalindrome=true;
				palindromesCount=palindromesCount+1;

			}else{
				isPalindrome=false;
				audioNo.Play();
			}
			Explode(other.gameObject.transform.position , isPalindrome); 
			objectCount();
				 
		}else if (other.gameObject.CompareTag ("spinner"))
		{
			while(!canSpawnHere){
				position = new Vector3(Random.Range(-22.0F, 22.0F), 0.5f, Random.Range(-22.0F, 22.0F));
                canSpawnHere = HPclass.checkSpawnPosition(position);
				count++;
				if(count==500)
				{Debug.Log("break");
					break;
				}
			}
			if(canSpawnHere){
				audiospin.Play();
				player.position=position;
				canSpawnHere=false;
			}
		}
		else if (other.gameObject.CompareTag ("bridge"))
		{
			audioBridge.Play();
		}
	}
	void Explode (Vector3 Explosionposition , bool isPalindrome) {
		if(isPalindrome){
			GameObject firework = Instantiate(RightExplosionPartical, Explosionposition, Quaternion.identity);
			firework.GetComponent<ParticleSystem>().Play();
		}else{
			GameObject firework = Instantiate(WrongExplosionPartical, Explosionposition, Quaternion.identity);
			firework.GetComponent<ParticleSystem>().Play();
		}
	}
	void objectCount ()
	{
		if (cubeCount == 0)
		{
			score.text=palindromesCount+"";
			completeLevelUI.SetActive(true); 
		}
	}
	 
 }
