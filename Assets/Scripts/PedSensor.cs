using UnityEngine;
using System.Collections;

public class PedSensor : MonoBehaviour {

	Pedestrian parentPedestrian;

	void Awake()
	{
		parentPedestrian = GetComponentInParent<Pedestrian> ();
	}

	// Use this for initialization
	void Start () {
		//parent = transform.parent
		//parentPedestrian = transform.parent.GetComponent<Pedestrian> ();
		//Debug.Log (parentPedestrian);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log (col.gameObject. + " " + col.gameObject.name);
		if (col.gameObject.layer == 9) {
			Debug.Log ("A pedestrian is close");
			parentPedestrian.numOfNearby++;
			parentPedestrian.nearbyPedestrians.Add(col.GetComponent<Pedestrian>());
		}
	}

	void OnTriggerExit(Collider col)
	{
		//Debug.Log (col.gameObject. + " " + col.gameObject.name);
		if (col.gameObject.layer == 9) {
			Debug.Log ("Pedestrian out of range");
			parentPedestrian.numOfNearby--;
			parentPedestrian.nearbyPedestrians.Remove(col.GetComponent<Pedestrian>());
		}
	}
}
