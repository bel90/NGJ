using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public GameObject otherPart;

	public bool isActivator;
	public bool inPickupRange;

	private void Update() {
		
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log($"Entered {gameObject.name}");
		if (!Inventory.instance.ItemInInventory(gameObject)) {
			Inventory.instance.currentItemActive = gameObject;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (Inventory.instance.currentItemActive == gameObject) {
			Inventory.instance.currentItemActive = null;
		}
	}
}
