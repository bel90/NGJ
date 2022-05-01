using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public static Character instance;

	private float walkingSpeed = 4f;
	private Rigidbody2D rig;

	public GameObject charAnimHolder;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	// Start is called before the first frame update
	void Start() {
		rig = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	private void FixedUpdate() {
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis("Horizontal");

		if (moveHorizontal < 0) {
			charAnimHolder.transform.rotation = Quaternion.Euler(0, 180, 0);
		} else if (moveHorizontal > 0) {
			charAnimHolder.transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		rig.velocity = new Vector2(moveHorizontal * walkingSpeed, 0);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log($"Trigger entered {collision.gameObject.name}");
		if (collision.gameObject.name == "Ghost") {
			GameEndManager.instance.GameOver();
		}
    }

	public void KillPlayer() {
		rig.velocity = Vector2.zero;
		Destroy(this);
	}
}
