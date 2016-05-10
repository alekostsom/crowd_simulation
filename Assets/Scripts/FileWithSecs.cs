using UnityEngine;
using System.Collections;

public class FileWithSecs : MonoBehaviour {

	string filename = "Time";
	System.IO.StreamWriter file;

	float secs = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine ("WriteInFile");

		
		// Write the string to a file.
		file = new System.IO.StreamWriter("C:\\Users\\Alexandros\\Documents\\" + filename + ".txt");
		file.WriteLine("Time in secs:");
		file.WriteLine(secs);	
	}
	
	void OnApplicationQuit() {
		file.Close ();
	}
	
	IEnumerator WriteInFile()
	{
		yield return new WaitForSeconds(0.5f);
		secs += 0.5f;

		file.WriteLine(secs);
		
		StartCoroutine ("WriteInFile");
	}

}
