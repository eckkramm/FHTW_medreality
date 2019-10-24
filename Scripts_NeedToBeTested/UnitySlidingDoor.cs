using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {

	public Transform doorTransform;
	public float raiseHeight = 3f;
	public float speed = 3f;
	private Vector3 _closedPosition;
	
	// Use this for initialization
	void Start () {
		// SET TO TRIGGER'S POSITION
		_closedPosition = transform.position;
	}
	
	void OnTriggerEnter (Collider other) {
		StopCoroutine("MoveDoor");
		Vector3 endpos = _closedPosition + new Vector3(0f, raiseHeight, 0f);
		StartCoroutine("MoveDoor", endpos);
	}
	
	void OnTriggerExit (Collider other) {
		StopCoroutine("MoveDoor");
		StartCoroutine("MoveDoor", _closedPosition);
	}
		
	IEnumerator MoveDoor (Vector3 endPos) {
		float t = 0f;
		Vector3 startPos = doorTransform.position;
		while (doorTransform.position != endPos) {
			doorTransform.position = Vector3.MoveTowards(startPos, endPos, Time.deltaTime * speed);
			yield return null;
		}
	}
}
