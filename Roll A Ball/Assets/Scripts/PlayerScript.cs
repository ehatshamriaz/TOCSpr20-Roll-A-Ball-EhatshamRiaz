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
	private float radius=2;
	public Transform player;
	private bool isPalindrome;

	
	void Start ()
	{
		rb = GetComponent<Rigidbody>(); 
		count = 0;
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
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			string str=other.transform.parent.gameObject.GetComponent<TextMesh>().text;
			count = count + 1;
			if(checkPalindrome(str))
			{
				isPalindrome=true;
				palindromesCount=palindromesCount+1;
				objectCount ();
			}
			Explode(other.gameObject.transform.position , isPalindrome); 
			Destroy(other.gameObject);
			Destroy(other.transform.parent.gameObject);

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
				player.position=position;
				canSpawnHere=false;
			}
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
		if (count >= 10)
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