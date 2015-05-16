using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class PlayerInputBehaviour : MonoBehaviour {
	public GameObject ballObject;

	// Use this for initialization
	void Start () {
		// this.OnMouseDownAsObservable ();
		Observable.EveryUpdate ()
			.Where (_ => Input.GetMouseButtonDown(0))
				.Select (_ => Input.mousePosition)
				.Subscribe (position => {
					position = Camera.main.ScreenToWorldPoint(position);
					Debug.Log ("touch:" + position.x + "," + position.y);
					this.ballObject.GetComponent<Rigidbody2D>().position = new Vector2(position.x, position.y);
				});
	}
}
