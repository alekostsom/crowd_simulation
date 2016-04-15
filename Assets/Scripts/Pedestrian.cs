using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {

	//Initial position and current position
	private Vector3 initPos = new Vector3();
	private Vector3 curPos = new Vector3();

	//Initial final destination and current destination
	private Vector3 finalTargetPos = new Vector3();
	private Vector3 curTargetPos = new Vector3();

	//Initial speed and current speed
	private float initSpeed;
	private float curSpeed;

	//Initial direction and current direction (in Euler Angles)
	private float initDir;
	private float curDir;

	//Pedestrian comfort zone, in meters
	private float radius;
	SphereCollider radSC;

	float timeIntervel = 5.0f;
	float distCovered = 0.0f;

	void Start () {
		Init ();
	}

	//Initialize pedestrian
	void Init()
	{
		initPos = new Vector3(Random.Range (-10, 10), transform.position.y, transform.position.z);
		curPos = initPos;
		transform.position = curPos;

		finalTargetPos.x = Random.Range (-10, 10);
		finalTargetPos.y = 0;
		finalTargetPos.z = Random.Range (-10, 10);
		finalTargetPos += initPos;
		curTargetPos = finalTargetPos;
		//Debug.Log (curPos + " " + curTargetPos);

		initSpeed = Random.Range (2, 10);
		timeIntervel = initSpeed;

		radSC = GetComponent<SphereCollider> ();
		radius = radSC.radius;

		Debug.Log (curPos + " " + curTargetPos + " " + initSpeed + " " + radius);
	}

	void Update () {
		distCovered += Time.deltaTime;
		transform.position = Vector3.Lerp (curPos, curTargetPos, distCovered/timeIntervel); 
	}

	public void UpdateSetup()
	{
		//yield return new WaitForSeconds(1.0f);
		/*curPos = new Vector3(Random.Range (-10, 10), transform.position.y, transform.position.z);
		Debug.Log (curPos);
		transform.position = curPos;*/
		//StartCoroutine (UpdateSetup ());
		curPos = transform.position;

		finalTargetPos.x = Random.Range (-10, 10);
		finalTargetPos.y = 0;
		finalTargetPos.z = Random.Range (-10, 10);
		finalTargetPos += curPos;
		curTargetPos = finalTargetPos;
		//Debug.Log (curPos + " " + curTargetPos);
		
		initSpeed = Random.Range (2, 10);
		timeIntervel = initSpeed;
		distCovered = 0.0f;		
		Debug.Log (curPos + " " + curTargetPos + " " + initSpeed + " " + radius);
	}
}
