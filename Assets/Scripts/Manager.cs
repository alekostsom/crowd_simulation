using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	public static Manager instance;

	//Pedestrian prefab model TODO or list of different models
	public GameObject pedPrefab;
	//public List<GameObject> pedPrefabs;
	//The list of the active pedestrians in the scenes
	List<Pedestrian> pedestrians = new List<Pedestrian>();
	//Number of pedestrians
	public int density = 16;

	//A reference to the hotspots parent object and an array to store them
	public GameObject destinationsParent;
	[HideInInspector]
	public Destination[] destinations;
	
	//A reference to the starting positions parent object and an array to store them
	public GameObject startPosParent;
	StartPos[] startingPositions;

	//How many seconds to spawn another pedestrian
	float timeToSpawnNext = 1.0f;
	public bool spawnAgain = true;

	//Write information to files
	string filenameDis = "Distance";
	System.IO.StreamWriter fileDis;

	string filenameTime = "Time";
	System.IO.StreamWriter fileTime;

	void Awake()
	{
		//Initialise singleton instance
		instance = this;
	}

	void Start () {
		//Store all the starting positions to the array
		startingPositions = startPosParent.GetComponentsInChildren<StartPos> ();
		//Debug.Log (startingPositions.Length); //Check if all the desired hotspots are included

		//Store all the hotspots to the array
		destinations = destinationsParent.GetComponentsInChildren<Destination> ();
		//Debug.Log (destinations.Length); //Check if all the desired hotspots are included

		//Now.. Instantiate pedestrians and add them to the active list
		for (int i=0; i<density; i++) {
			InstantiatePedestrian();
		}

		StartCoroutine (SpawPedestrian ());
		StartCoroutine (StopSpawning ());

		//Do something for every pedestrian?

		//Open files to store information
		//filenameDis += " " + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + " " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
		fileDis = new System.IO.StreamWriter("C:\\Users\\Alexandros\\Documents\\" + filenameDis + ".txt");
		fileTime = new System.IO.StreamWriter("C:\\Users\\Alexandros\\Documents\\" + filenameTime + ".txt");
	}

	void OnApplicationQuit() {
		fileDis.Close ();
		fileTime.Close ();
	}

	public void WriteDis(float dis, float time)
	{
		fileDis.WriteLine(dis);
		fileTime.WriteLine (time);
	}

	//Create a new Pedestrian object and place it at one of the available starting positions
	void InstantiatePedestrian()
	{
		//Randomly pick a starting position
		int startPosIndex = Random.Range(0, startingPositions.Length);
		
		Vector3 startPos = startingPositions[startPosIndex].transform.position; 
		//Instantiate the pedestrian prefab
		GameObject go = GameObject.Instantiate(pedPrefab, startPos, Quaternion.identity) as GameObject;
		
		Pedestrian ped = go.GetComponent<Pedestrian>();//Get the pedestrian component reference
		pedestrians.Add(ped); //Add the pedestrian component to the list
		ped.StartingPos = startPos; //Assign the starting Position
	}

	//Spawn another pedestrian regularly
	IEnumerator SpawPedestrian()
	{
		yield return new WaitForSeconds(timeToSpawnNext);
		InstantiatePedestrian ();

		timeToSpawnNext = Random.Range (0.05f, 0.25f);

		if (spawnAgain)
			StartCoroutine (SpawPedestrian ());
	}

	//Stop spawning new pedestrians after a specific amount of time
	IEnumerator StopSpawning()
	{
		yield return new WaitForSeconds (60);
		spawnAgain = false;
	}
}
