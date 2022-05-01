using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour {

	bool playerInSafespot = false;
	bool ghostInSafespot = false;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name == "Character") {
			playerInSafespot = true;

			if (ghostInSafespot) {
				Ghost.instance.VanishGhost();
			}
		}

		if (collision.gameObject.name == "Ghost") {
			ghostInSafespot = true;

			if (playerInSafespot) {
				Ghost.instance.VanishGhost();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.name == "Character") {
			playerInSafespot = false;
		}

		if (collision.gameObject.name == "Ghost") {
			ghostInSafespot = false;
		}
	}
}
