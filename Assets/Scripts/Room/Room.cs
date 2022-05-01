using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public Border leftBorder;
	public Border rightBorder;

	float borderOffset = 2;

	public void LoadRoomLeft() {
		RoomManager.instance.LoadRoomLeft();
	}

	public void LoadRoomRight() {
		RoomManager.instance.LoadRoomRight();
	}

	public float GetEntryPosition(Position position) {
		if (position == Position.Left) {
			return GetLeftEntryPosition();
		}
		return GetRightEntryPosition();
	}

	public float GetLeftEntryPosition() {
		return leftBorder.transform.position.x + borderOffset;
		//return transform.position.x + leftBorder.transform.position.x + borderOffset;
	}

	public float GetRightEntryPosition() {
		return rightBorder.transform.position.x - borderOffset;
		//return transform.position.x + rightBorder.transform.position.x - borderOffset;
	}

}
