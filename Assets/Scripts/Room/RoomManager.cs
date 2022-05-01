using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public static RoomManager instance;

	public GameObject[] roomList;
	[HideInInspector] public int currentRoom = 0;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	//For character
	public void LoadRoomLeft() {
		int nextRoom = currentRoom == 0 ? roomList.Length - 1 : currentRoom - 1;
		currentRoom = nextRoom;
		LoadRoom(nextRoom, Position.Right);
	}

	public void LoadRoomRight() {
		int nextRoom = currentRoom == roomList.Length - 1 ? 0 : currentRoom + 1;
		currentRoom = nextRoom;
		LoadRoom(nextRoom, Position.Left);
	}

	public void LoadRoom(int roomIndex, Position position) {
		Vector3 charPos = Character.instance.gameObject.transform.position;
		float xPos = roomList[roomIndex].GetComponent<Room>().GetEntryPosition(position);
		Character.instance.gameObject.transform.position = new Vector3(xPos, charPos.y, charPos.z);
		SongSoundManager.instance.OpenDoor();
	}

	//For Ghost
	public float GetNextRoomGhostPosition(int nextRoom, Position direction) {
		//reverse direction
		direction = direction == Position.Left ? Position.Right : Position.Left;
		return roomList[nextRoom].GetComponent<Room>().GetEntryPosition(direction);
	}

	public int GetNextRoomIndex(int thisRoom, Position direction) {
		if (direction == Position.Left) {
			return thisRoom == 0 ? roomList.Length - 1 : thisRoom - 1;
		}
		return thisRoom == roomList.Length - 1 ? 0 : thisRoom + 1;
	}

	public float GetDistanceToBorder(float xPos, Position pos, int roomIndex) {
		float pos1 = 0;
		if (pos == Position.Left) {
			pos1 = roomList[roomIndex].GetComponent<Room>().leftBorder.gameObject.transform.position.x;
		} else {
			pos1 = roomList[roomIndex].GetComponent<Room>().rightBorder.gameObject.transform.position.x;
		}
		

		return Mathf.Abs(pos1 - xPos);
	}
}
