using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour
{
	// spawn a new teddy each ... seconds
	public float interval = 3.0f;
	float timeLeft = 0.0f;
	
	// gameobject to be spawned
	public GameObject teddy = null;
	
	// destination (where all spawned objects run to)
	public Transform destination = null;
	
	// Update is called once per frame
	void Update ()
	{
		// time to spawn the next one?
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0.0f)
		{
			// spawn
			GameObject g = (GameObject)Instantiate(teddy, transform.position, Quaternion.identity);
			
			// get access to the navmesh agent component
			NavMeshAgent n = g.GetComponent<NavMeshAgent>();
			n.destination = destination.position;
			
			// reset time
			timeLeft = interval;
		}
	}
}