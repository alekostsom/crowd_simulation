using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	public List<Pedestrian> pedestrians;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUILayout.Button("Update Destinations"))
		{
			foreach (Pedestrian pedestrian in pedestrians)
				pedestrian.UpdateSetup();
		}
	}
}
