using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {
	private bool isWarping;
	
	// Use this for initialization
	void Start () {
		isWarping = false;
	}

	public bool GetIsWarping() {
		return isWarping;
	}

	public void SetIsWarping(bool warping) {
		isWarping = warping;
	}
}
