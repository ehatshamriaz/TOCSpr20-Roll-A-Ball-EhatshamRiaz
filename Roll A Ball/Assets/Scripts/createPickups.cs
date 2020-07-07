using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using roll_a_ball_helper;

public class createPickups : MonoBehaviour
{

    HelperClass HpClass = new HelperClass(); 
	public GameObject Cube ,floatingtext;
	private Vector3 position;
	private bool canSpawnHere;
	private int count,strSize;
	private string randomString; 
	private string[] randomStrings;
	void Start()
    {
		randomStrings= new string[10];
		for(int y=0;y<3;y++){
            randomStrings[y] = HpClass.createPalindromeString(Random.Range(9, 15));
		}
		for(int y=3;y<10;y++){
			int j = Random.Range(0, 2);
			if(j== 0){
                randomStrings[y] = HpClass.createString(Random.Range(9, 15));
			}
			else{
                randomStrings[y] = HpClass.createPalindromeString(Random.Range(9, 15));
			}

		}
		for(int i=0;i<10;i++)
		{  
			while(!canSpawnHere){
				position = new Vector3(Random.Range(-22.0F, 22.0F), 0.5f, Random.Range(-22.0F, 22.0F));
				//Debug.Log(randomString);
                canSpawnHere = HpClass.checkSpawnPosition(position);
				count++;
				if(count==1000)
			{Debug.Log("break");
					break;
				}
			}
			if(canSpawnHere){
				GameObject go =Instantiate(Cube,position,Quaternion.identity)as GameObject;
				GameObject text =Instantiate(floatingtext,new Vector3(position.x,position.y+1.0f,position.z),Quaternion.identity)as GameObject;
				go.transform.SetParent(text.transform);
				text.GetComponent<TextMesh>().text=randomStrings[i];
				canSpawnHere=false;
			}


		}
	}
	 
}