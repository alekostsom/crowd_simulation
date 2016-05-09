using UnityEngine;
using System.Collections;

public class TestNavMesh : MonoBehaviour {

	public GameObject target; 

	NavMeshAgent agent;
	Vector3 forward;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		//Draw direction
		forward = transform.TransformDirection(Vector3.forward) * 2;
		Debug.DrawRay (transform.position, forward, Color.green);
		//Debug.Log (agent.stoppingDistance);
		if (!agent.pathPending)
		{
			//if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					Debug.Log("Reached");
					// Done
				}
			}
		}
	}
}
