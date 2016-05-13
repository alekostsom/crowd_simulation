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

	//Pedestrian comfort zone, in meters (r)
	private float radius;
	SphereCollider radSC;

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

	//Evaluation variables
	//float timeToReachGoal;
	float totalPathLength;
	float avgSpeed;
	float totalAcceleration; // movement effort
	float totalAngleDegrees; // turning effort

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
		//Pick random color for the pedestrian
		Color32 color = new Color32 ((byte)Random.Range (0, 255), (byte)Random.Range (0, 255), (byte)Random.Range (0, 255), 1);
		GetComponentInChildren<MeshRenderer> ().material.color = color;
		//psomakiPrefab.GetComponent<MeshRenderer> ().material.color = color;

		radSC = GetComponent<SphereCollider> ();
		radius = radSC.radius;

		if (target != null)
			finalTargetPos = target.transform.position;

		agent.SetDestination (finalTargetPos);
	}

	void Update () {
		//distCovered += Time.deltaTime;
		//transform.position = Vector3.Lerp (curPos, curTargetPos, distCovered/timeIntervel); 
		//transform.Translate (Vector3.forward * Time.deltaTime, Space.Self);
		//transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 10);

		//Debug.Log (agent.velocity.ToString() + agent.desiredVelocity.ToString());
		if (isInPath) {
			timeToReachGoal += Time.deltaTime;
		}

	}

	public void UpdateSetup()
	{
	}

	//Actions to be taken when the pedestrian reaches his goal
	public void ReachedGoal()
	{
		isInPath = false;
		Debug.Log ("Covered: " +CalcPathLengthInMeters () + " meters, in: " + timeToReachGoal + " seconds.");
		managerInstance.WriteDis (CalcPathLengthInMeters ());
	}

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

	public IEnumerator LeaveTraces()
	{
		traces.Add (new Vector2 (transform.position.x, transform.position.z));
		//Debug.Log ("traces: " + traces.Count);
		//Instantiate (psomakiPrefab, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		StartCoroutine (LeaveTraces ());
	}
}
