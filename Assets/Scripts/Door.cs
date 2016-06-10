using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour { 
	//Door Objects that determine whether the pedestrian reached its destination or not.

	GameObject pedGO;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Pedestrian") {
			//Debug.Log ("Reached");
			col.GetComponent<Pedestrian>().ReachedGoal();
			Destroy(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag != "Pedestrian" && col.gameObject.tag != "PedSensor") {

			col.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
			col.gameObject.GetComponent<Pedestrian>().IsInPath = true;
			StartCoroutine( col.gameObject.GetComponent<Pedestrian>().LeaveTraces() );

			//StartCoroutine(col.gameObject.GetComponent<Pedestrian>().EnableExiting());
			//Destroy(col.gameObject);
		}
	}


}
