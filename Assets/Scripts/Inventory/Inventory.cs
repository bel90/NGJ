using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory instance;

	int inventorySize = 1;
	private List<Item> allItems = new List<Item>();
    private List<Item> inventoryItems = new List<Item>();

    [HideInInspector] public GameObject currentItemActive; //For picking up or interacting with an item

	public GameObject winningStick;
	public GameObject musicBox;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

	private void Start() {
		//foreach (Transform item in transform.GetChildCount())
		for (int i = 0; i < transform.childCount; i++) {
            allItems.Add(transform.GetChild(i).gameObject.GetComponent<Item>());
		}
	}

	private void Update() {
		if (currentItemActive != null) {
			if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) {
				if (currentItemActive.GetComponent<Item>().isActivator) {
					//try to pick it up
					if (inventoryItems.Count <= inventorySize) {
						inventoryItems.Add(currentItemActive.GetComponent<Item>());
						currentItemActive.transform.parent = Character.instance.transform;
					}
				} else {
					//check if activator is in inventory
					if (inventoryItems.Count >= 1 && inventoryItems[0].otherPart == currentItemActive) {
						//TODO activate
						if (currentItemActive.CompareTag("Door")) {
							SongSoundManager.instance.UnlockDoor();
						}
						if (currentItemActive.CompareTag("Candle")) {
							SafeSpot.instance.gameObject.SetActive(true);
							winningStick.SetActive(true);
							musicBox.SetActive(true);
						}
						if (currentItemActive.CompareTag("Winning")) {
							GameEndManager.instance.Win();
						}

						Destroy(currentItemActive);
						Destroy(inventoryItems[0].gameObject);
						inventoryItems.RemoveAt(0);
					}
				}
			}
		}

		if (currentItemActive == null && inventoryItems.Count >= 1) {
			if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) {
				inventoryItems[0].transform.parent = transform;
				inventoryItems.RemoveAt(0);
			}
		}
	}

	public bool ItemInInventory(GameObject item) {
		if (inventoryItems.Count >= 1 && inventoryItems[0].gameObject == item) {
			return true;
		}

		return false;
	}
}
