using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	
	void Update () 
	{
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		transform.Rotate (new Vector3 (45, 30, 15) * Time.deltaTime);
	}
}