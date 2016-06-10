using UnityEngine;
using System.Collections;

public class EnterDoor : MonoBehaviour {
	//Door Objects that determine whether the pedestrian has entered the park area or not

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Pedestrian" && col.gameObject.tag != "PedSensor") {

			//if (col.gameObject != null)
				col.gameObject.tag = "Pedestrian";
		}
	}


}
