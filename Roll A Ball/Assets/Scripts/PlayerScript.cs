using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public GameObject RightExplosionPartical,WrongExplosionPartical;
	public float speed; 
	public Text score;
	public GameObject completeLevelUI;
	private Vector3 position;
	private Rigidbody rb;
	private int count ,palindromesCount;
	private bool canSpawnHere;
	public Collider[] colliders; 
	public Transform player;
	private bool isPalindrome;
	private int cubeCount ;
	public Text cubeCountText;
	public AudioSource audioSource ,audioNo,audiospin,audioBridge;
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
			if(checkPalindrome(str))
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
				canSpawnHere=checkSpawnPosition(position);
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
	  bool checkPalindrome(string str){  
			int l = str.Length;
			for (int i = 0; i < l/2; i++) {
				if (str[i] != str[l - 1 - i]) {
					return false;
				}
			}
			return true;
		 
	}
	void objectCount ()
	{
		if (cubeCount == 0)
		{
			CompleteLevel(); 
		}
	}
	void CompleteLevel(){
		score.text=palindromesCount+"";
		completeLevelUI.SetActive(true);
	}
	bool checkSpawnPosition( Vector3 SpawnPos)
	{
		colliders= Physics.OverlapSphere(position,1);
		for(int i=0; i<colliders.Length;i++){
			if(colliders[i].tag=="ground"){
				continue;
			}
			Vector3 centerPoint=colliders[i].bounds.center;
			float width = colliders[i].bounds.extents.x;
			float height= colliders[i].bounds.extents.z;
			
			float leftExtent=(centerPoint.x - width)-5;
			float rightExtent=(centerPoint.x + width)+5;
			float upExtent=(centerPoint.z - height)-5;
			float downExtent=(centerPoint.z + height)+5;
			if(SpawnPos.x >= leftExtent && SpawnPos.x <= rightExtent){
				if(SpawnPos.z >= upExtent && SpawnPos.z <= downExtent){
					return false;
				}
			}
			
		}
		return true;
	}
}