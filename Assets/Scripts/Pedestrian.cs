using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour {

	//Reference to manager
	Manager managerInstance; 

	//Initial position and current position
	private Vector3 startingPos = new Vector3();
	public Vector3 StartingPos
	{
		get { return startingPos;}
		set { startingPos = value;}
	}
	private Vector3 curPos = new Vector3();

	//Initial final destination and current destination (GOAL)
	private Vector3 finalTargetPos = new Vector3();
	public Vector3 FinalTargetPos
	{
		get { return finalTargetPos; }
		set { finalTargetPos = value;}
	}
	private Vector3 curTargetPos = new Vector3();

	//Initial speed and current speed
	private float initSpeed;
	private float curSpeed;

	//Initial direction and current direction (in Euler Angles)
	private float initDir;
	private float curDir;



	//Variables that count the pedestrian's path in seconds
	float timeToReachGoal = 0.0f;
	bool isInPath = false;
	public bool IsInPath
	{
		get { return isInPath; }
		set { isInPath = value; }
	}
	float timeIntervel = 5.0f;
	float distCovered = 0.0f;

	//List of all the objects that are close
	List<GameObject> closeObjects = new List<GameObject>();

	//Prefab that will be continuasly instatiated, so the path is visible
	public GameObject psomakiPrefab;
	List<Vector2> traces = new List<Vector2> ();

	public GameObject target; 
	
	NavMeshAgent agent;
	public NavMeshAgent Agent
	{
		get { return agent;}
	}

	//Animation-related objects
	//TODO


	Vector3 forward;

	//A list that contains only the possible target positions for the pedestrian
	List<Vector3> desPosList = new List<Vector3> ();

	//Evaluation variables
	//float timeToReachGoal;
	float totalPathLength;
	float avgSpeed;
	float totalAcceleration; // movement effort
	float totalAngleDegrees; // turning effort

	//Socially realistic avoidance (Karamouzas 2010 model)

	//Pedestrian comfort zone, in meters (r)
	float radius;
	public SphereCollider radSC;
	public Transform visualRange;

	public int numOfNearby = 0;
	public List<Pedestrian> nearbyPedestrians = new List<Pedestrian>();
	List<Pedestrian> onCollisionCoursePedestrians = new List<Pedestrian>();


	void Awake(){
		//Get a reference to navMesh component. Componet got in Awake because the object is instantiated on realtime.
		agent = GetComponent<NavMeshAgent> ();

		managerInstance = Manager.instance;
	}

	void Start () {
		Init ();
	}

	//Initialize pedestrian
	void Init()
	{
		//Pick random color for the pedestrian for a visual variety
		Color32 color = new Color32 ((byte)Random.Range (0, 255), (byte)Random.Range (0, 255), (byte)Random.Range (0, 255), 1);
		GetComponentInChildren<MeshRenderer> ().material.color = color;
		//psomakiPrefab.GetComponent<MeshRenderer> ().material.color = color;

		//radSC = GetComponent<SphereCollider> ();
		radius = Random.Range (5.0f, 12.0f);
		radSC.radius = radius;
		if (visualRange != null) {
			Vector3 sphereSize = new Vector3 (radius * 2, 0.1f, radius * 2);
			visualRange.localScale = sphereSize;
		}
		//Create the list with the possible targets (except the pedestrians' startingPos)
		foreach (Destination des in managerInstance.destinations) {
			if (des.transform.position != startingPos) //exclude the same position with the starting
			{
				desPosList.Add(des.transform.position);
			}
		}

		//Choosea  destination for each pedestrian from the available ones
		foreach (Vector3 desPos in desPosList)
		{			
			finalTargetPos = desPosList[Random.Range(0, desPosList.Count)];
		}

		//Spread the pedestrians a little around their initial position
		float randX = Random.Range(-4.0f, 4.0f);
		float randZ = Random.Range(-4.0f, 4.0f);
		transform.position = new Vector3(startingPos.x + randX, startingPos.y, startingPos.z + randZ);

		agent.speed = Random.Range (2.5f, 4.5f);
		agent.SetDestination (finalTargetPos);

		StartCoroutine (CheckForImminentCollisions ());
	}

	void Update () {

		//Calculate the amount time it takes for a pedestrian to reach his goal
		if (isInPath) {
			timeToReachGoal += Time.deltaTime;
		}

		/*if (Input.GetKeyDown(KeyCode.Space))
			agent.velocity = Vector3.zero;

		if (Input.GetKeyDown(KeyCode.Return))
			agent.velocity = Vector3.forward;*/
	}

	//Actions to be taken when the pedestrian reaches his goal
	public void ReachedGoal()
	{
		isInPath = false;
		Debug.Log ("Covered: " +CalcPathLengthInMeters () + " meters, in: " + timeToReachGoal + " seconds. \nFrom: " + startingPos + " to: " + finalTargetPos);
		managerInstance.WriteDis (CalcPathLengthInMeters (), timeToReachGoal);
	}

	//Calculate the distance that the pedestrian covered.
	float CalcPathLengthInMeters() {

		if (traces.Count < 2)
			return 0;
		
		Vector2 previousPos = traces[0];
		float lengthSoFar = 0.0F;
		int i = 1;
		while (i < traces.Count) {
			Vector2 currentPos = traces[i];
			lengthSoFar += Vector3.Distance(previousPos, currentPos);
			previousPos = currentPos;
			i++;
		}
		return lengthSoFar;
	}

	//Create an array of path positions
	public IEnumerator LeaveTraces()
	{
		traces.Add (new Vector2 (transform.position.x, transform.position.z));

		//Debug.Log ("traces: " + traces.Count);
		//Instantiate (psomakiPrefab, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		StartCoroutine (LeaveTraces ());
	}

	//Wait 1.5 seconds to enable the triggering, because some times the pedestrian exits and re-enters the doors
	/*public IEnumerator EnableExiting()
	{
		yield return new WaitForSeconds(1.5f);
		
	}*/	

	public IEnumerator CheckForImminentCollisions()
	{
		yield return new WaitForSeconds(1.0f);
		/*foreach (Pedestrian nearPed in nearbyPedestrians) {
			//The imminent collision detection code goes here
			Debug.Log (nearPed.Agent.velocity + " " + nearPed.transform.position);

			float timeToCol;
			float minDistance = 4;

			timeToCol = (minDistance - (Vector3.Distance(nearPed.transform.position, transform.position)))/(Vector3.Distance(nearPed.Agent.velocity, agent.desiredVelocity));
			Debug.Log (timeToCol);
		}*/

		StartCoroutine (CheckForImminentCollisions ());
	}

}