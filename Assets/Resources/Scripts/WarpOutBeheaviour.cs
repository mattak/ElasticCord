using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class WarpOutBeheaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		this.gameObject.OnTriggerEnter2DAsObservable ()
			.Where (collider => collider.gameObject.tag == "Ball")
				.Select (collider => collider.gameObject.GetComponent<BallBehaviour>())
				.Where (ball => ball.GetIsWarping())
				.Subscribe (ball => {
					ball.SetIsWarping (false);
				});
				*/
	}
}