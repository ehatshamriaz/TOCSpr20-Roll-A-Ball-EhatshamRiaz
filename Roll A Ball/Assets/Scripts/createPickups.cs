using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class createPickups : MonoBehaviour
{
	public Transform spawner;
	public GameObject prefab ,floatingtext;
	public Collider[] colliders; 
	private Vector3 position;
	private bool canSpawnHere;
	private int count,strSize;
	private string randomString; 
	private string[] randomStrings;
	void Start()
    {
		randomStrings= new string[10];
		for(int y=0;y<3;y++){
			randomStrings[y]=createPalindromeString(Random.Range(9, 15));
		}
		for(int y=3;y<10;y++){
			int j = Random.Range(0, 2);
			if(j== 0){
				randomStrings[y]=createString(Random.Range(9, 15));
			}
			else{
				randomStrings[y]=createPalindromeString(Random.Range(9, 15));
			}

		}

		for(int i=0;i<10;i++)
		{  
			while(!canSpawnHere){
				position = new Vector3(Random.Range(-22.0F, 22.0F), 0.5f, Random.Range(-22.0F, 22.0F));
				//Debug.Log(randomString);
				canSpawnHere=checkSpawnPosition(position);
				count++;
				if(count==1000)
			{Debug.Log("break");
					break;
				}
			}
			if(canSpawnHere){
				GameObject go =Instantiate(prefab,position,Quaternion.identity)as GameObject;
				GameObject text =Instantiate(floatingtext,new Vector3(position.x,position.y+1.0f,position.z),Quaternion.identity)as GameObject;
				go.transform.SetParent(text.transform);
				text.GetComponent<TextMesh>().text=randomStrings[i];
				canSpawnHere=false;
			}


		}
	}
	 
	public string createPalindromeString(int size)
	{
		
		char[] str = new char[size];
		 
		int end = size - 1;
		int i = 1;
		for (i = 0; i < size; i++)
		{
			int x = Random.Range(0, 3);
			if (x == 0)
			{
				str[i] = 'X';
				str[end] = 'X';
			}
			else if(x == 1)
			{
				str[i] = 'E';
				str[end] = 'E';
				
			}else{
				
				str[i] = '6';
				str[end] = '6';
			}
			end--;
			if (end <= i)
			{
				break;
			}
		}
		
		return new string(str);
	}
	//create random strings
	public string createString(int size)
	{
		
		char[] str = new char[size];
		
		int end = size - 1;
		int i = 1;
		for (i = 0; i < size; i++)
		{
			int x = Random.Range(0, 3);
			if (x == 0)
			{
				int j = Random.Range(0, 2);
				if(j== 0){
					str[i] = 'X';
					str[end] = 'X';}
				else{
					str[i] = 'X';
					str[i+1] = 'X';
					i++;
				}
			}
			else if(x == 1)
			{
				int j = Random.Range(0, 2);
				if(j== 0){
					str[i] = 'E';
					str[end] = 'E';}
				else{
					str[i] = 'E';
					str[i+1] = 'E';
					i++;
				}
				
			}else{
				int j = Random.Range(0, 2);
				if(j== 0){
					str[i] = '6';
					str[end] = '6';}
				else{
					str[i] = '6';
					str[i+1] = '6';
					i++;
				}
			}
			end--;
			if (end <= i)
			{
				break;
			}
		}
		
		return new string(str);
	}
	// Update is called once per frame
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