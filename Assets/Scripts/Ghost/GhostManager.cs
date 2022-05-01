using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour {

	public static GhostManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        SpawnGhostInSec(10);
    }

    public void SpawnGhostInSec(float secs) {
        StartCoroutine(SpawnGhost(secs));
	}

    private IEnumerator SpawnGhost(float secs) {
        yield return new WaitForSeconds(secs);

        Ghost.instance.SpawnGhost();
	}
}
