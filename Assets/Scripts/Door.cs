using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour { //Door Objects that determine whether the pedestrian reached its destination or not.


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Pedestrian") {
			Debug.Log ("Reached");
			Destroy(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag != "Pedestrian") {
			Debug.Log ("Exited");
			col.gameObject.tag = "Pedestrian";
			col.GetComponent<MeshRenderer>().enabled = true;
			//Destroy(col.gameObject);
		}
	}
}
