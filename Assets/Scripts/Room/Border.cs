using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    public Room parentRoom;
    public Position borderPosition;

    // Start is called before the first frame update
    void Start() {
        
    }

	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Character") {
            if (borderPosition == Position.Left) {
                parentRoom.LoadRoomLeft();
            }
            if (borderPosition == Position.Right) {
                parentRoom.LoadRoomRight();
            }
		}

        if (collision.gameObject.name == "Ghost") {
            Ghost.instance.MoveGhostToRoom(borderPosition);
		}
	}
}

public enum Position {
    Left,
    Right
}