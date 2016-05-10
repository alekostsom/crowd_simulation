using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour { 
	//Door Objects that determine whether the pedestrian reached its destination or not.



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
		if (col.gameObject.tag != "Pedestrian") {
			//Debug.Log ("Exited");
			col.gameObject.tag = "Pedestrian";
			col.GetComponentInChildren<MeshRenderer>().enabled = true;
			col.GetComponent<Pedestrian>().IsInPath = true;
			StartCoroutine( col.GetComponent<Pedestrian>().LeaveTraces() );
			//Destroy(col.gameObject);
		}
	}
}
