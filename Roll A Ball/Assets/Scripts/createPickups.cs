using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPickups : MonoBehaviour
{
	public Transform spawner;
	public GameObject prefab ,floatingtext;
	public Collider[] colliders;
	float radius=2;
	Vector3 position;
	bool canSpawnHere;
	int count;
    // Start is called before the first frame update
    void Start()
    {
		for(int i=0;i<10;i++)
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
				GameObject go =Instantiate(prefab,position,Quaternion.identity)as GameObject;
				GameObject text =Instantiate(floatingtext,new Vector3(position.x,position.y+1.0f,position.z),Quaternion.identity)as GameObject;
				go.transform.SetParent(text.transform);
				text.GetComponent<TextMesh>().text="e3x3e";
				canSpawnHere=false;
			}


		}
	}
	void createString(){


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