using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	//Pedestrian prefab model TODO or list of different models
	public GameObject pedPrefab;
	//public List<GameObject> pedPrefabs;
	//The list of the active pedestrians in the scenes
	List<Pedestrian> pedestrians = new List<Pedestrian>();
	//Number of pedestrians
	public int density = 16;

	//A reference to the hotspots parent object and an array to store them
	public GameObject destinationsParent;
	Destination[] destinations;
	
	//A reference to the starting positions parent object and an array to store them
	public GameObject startPosParent;
	StartPos[] startingPositions;


	void Start () {
		//Store all the starting positions to the array
		startingPositions = startPosParent.GetComponentsInChildren<StartPos> ();
		Debug.Log (startingPositions.Length); //Check if all the desired hotspots are included

		//Store all the hotspots to the array
		destinations = destinationsParent.GetComponentsInChildren<Destination> ();
		Debug.Log (destinations.Length); //Check if all the desired hotspots are included

		//Now.. Instantiate pedestrians and add them to the active list
		for (int i=0; i<density; i++) {
			Vector3 startPos = startingPositions[Random.Range(0, startingPositions.Length)].transform.position; //Randomly pick a starting position
			GameObject go = GameObject.Instantiate(pedPrefab, startPos, Quaternion.identity) as GameObject;

			pedestrians.Add(go.GetComponent<Pedestrian>()); //Add the pedestrian component to the list

			//go.GetComponent<Pedestrian>().agent.SetDestination(destinations[Random.Range(0, destinations.Length)].transform.position);
		}
		Debug.Log (pedestrians.Count);

		//Do something for every pedestrian
		foreach (Pedestrian ped in pedestrians)
		{
			//Set the destination for each pedestrian
			ped.Agent.SetDestination (destinations[Random.Range(0, destinations.Length)].transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
