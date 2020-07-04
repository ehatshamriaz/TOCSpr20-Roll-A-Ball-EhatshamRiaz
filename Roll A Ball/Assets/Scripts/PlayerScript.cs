using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;
	private Vector3 position;
	private Rigidbody rb;
	private int count;
	private bool canSpawnHere;
	public Collider[] colliders;
	float radius=2;
	public Transform player;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>(); 
		count = 0;
		SetCountText ();
		winText.text = "";

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
			Debug.Log(str);
			if(checkPalindrome(str))
			{
				//other.gameObject.SetActive (false);
				Destroy(other.gameObject);
				Destroy(other.transform.parent.gameObject);
				count = count + 1;
				SetCountText ();
			}
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
	bool checkPalindrome(string str){ 
			int l = str.Length;
			for (int i = 0; i < l/2; i++) {
				if (str[i] != str[l - 1 - i]) {
					return false;
				}
			}
			return true;
		 
	}
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 8)
		{
			winText.text = "You Win!";
		}
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