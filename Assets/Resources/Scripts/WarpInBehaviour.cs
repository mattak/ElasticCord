using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class WarpInBehaviour : MonoBehaviour {
	public WarpOutBeheaviour warpToObject;

	// Use this for initialization
	void Start () {
		this.gameObject.OnTriggerEnter2DAsObservable ()
			.Where (collider => collider.gameObject.tag == "Ball")
			.Select (collider => collider.gameObject.GetComponent<BallBehaviour>())
			//.Where (ball => !ball.GetIsWarping())
			.Subscribe (ball => {
					ball.SetIsWarping (true);
					Vector3 warpPosition = warpToObject.transform.position;
					ball.gameObject.transform.position = new Vector3 (warpPosition.x, warpPosition.y, ball.gameObject.transform.position.z);					                                                   
			});
	}
}