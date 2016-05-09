using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour {

	//Initial position and current position
	private Vector3 initPos = new Vector3();
	private Vector3 curPos = new Vector3();

	//Initial final destination and current destination (GOAL)
	private Vector3 finalTargetPos = new Vector3();
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

	float timeIntervel = 5.0f;
	float distCovered = 0.0f;

	//List of all the objects that are close
	List<GameObject> closeObjects = new List<GameObject>();


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
	float timeToReachGoal;
	float totalPathLength;
	float avgSpeed;
	float totalAcceleration; // movement effort
	float totalAngleDegrees; // turning effort


	void Awake(){
		//Get a reference to navMesh component. Componet got in Awake because the object is instantiated on realtime.
		agent = GetComponent<NavMeshAgent> ();
	}

	void Start () {
		Init ();
	}

	//Initialize pedestrian
	void Init()
	{


		//Pick a random starting point
		//Random.seed = (int)Time.deltaTime;
		initPos = new Vector3(Random.Range (-10, 10), transform.position.y, transform.position.z);
		curPos = initPos;
		//transform.position = curPos;

		//Calculate target position
		finalTargetPos.x = Random.Range (-10, 10);
		finalTargetPos.y = 1;
		finalTargetPos.z = Random.Range (-10, 10);
		finalTargetPos += initPos;
		curTargetPos = finalTargetPos;

		/*initSpeed = Random.Range (2, 10);
		timeIntervel = initSpeed;*/

		radSC = GetComponent<SphereCollider> ();
		radius = radSC.radius;

		//agent.SetDestination (target.transform.position);
		//Debug.Log (curPos + " " + curTargetPos + " " + initSpeed + " " + radius);
	}

	void Update () {
		//distCovered += Time.deltaTime;
		//transform.position = Vector3.Lerp (curPos, curTargetPos, distCovered/timeIntervel); 
		//transform.Translate (Vector3.forward * Time.deltaTime, Space.Self);
		//transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 10);

		//Debug.Log (agent.velocity.ToString() + agent.desiredVelocity.ToString());
	}

	public void UpdateSetup()
	{
	}

}
