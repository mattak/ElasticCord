using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class PlayerInputBehaviour : MonoBehaviour {
	public GameObject ballObject;

	// Use this for initialization
	void Start () {
		// this.OnMouseDownAsObservable ();
		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonDown(0)))
				.Subscribe (position => {
					Rigidbody2D rigidbody = this.ballObject.GetComponent<Rigidbody2D>();
					rigidbody.isKinematic = true;
				});

		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonUp(0)))
			.Subscribe (position => {
				Rigidbody2D rigidbody = this.ballObject.GetComponent<Rigidbody2D>();
				rigidbody.isKinematic = false;
			});

		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButton(0)))
			.Subscribe (position => {
				Rigidbody2D rigidbody = this.ballObject.GetComponent<Rigidbody2D>();
				rigidbody.position = position;
			});
	}

	IObservable<Vector2> CreateMouseObservable (IObservable<long> observable) {
		return observable
				.Select (_ => Input.mousePosition)
				.Select (position => Camera.main.ScreenToWorldPoint(position))
				.Select (position => new Vector2(position.x, position.y));
	}
}
