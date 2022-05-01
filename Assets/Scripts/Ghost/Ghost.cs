using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public static Ghost instance;

    public Transform spawnPosition;

    Position moveDirection;
    bool isChasing;

    private float walkingSpeed = 2f;
    private Rigidbody2D rig;

    public GameObject charAnimHolder;

    int ghostRoom = 1;

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
        isChasing = true;
        VanishGhost(false);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (isChasing) {
            float moveHorizontal = GetGhostDirection();

            if (moveHorizontal < 0) {
                charAnimHolder.transform.rotation = Quaternion.Euler(0, 180, 0);
            } else if (moveHorizontal > 0) {
                charAnimHolder.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            rig.velocity = new Vector2(moveHorizontal * walkingSpeed, 0);
        }
        
    }

    public void SpawnGhost() {
        Vector3 curPos = transform.position;
        transform.position = new Vector3(spawnPosition.position.x, curPos.y, curPos.z);
        ghostRoom = 1;
        SongSoundManager.instance.GhostSpawned();

        gameObject.SetActive(true);
    }

    public void VanishGhost(bool newSpawn = true) {
        if (newSpawn) GhostManager.instance.SpawnGhostInSec(Random.Range(15, 40));
        SongSoundManager.instance.GhostDespawned();
        gameObject.SetActive(false);
	}

    private float GetGhostDirection() {
        if (RoomManager.instance.currentRoom == ghostRoom) {
            SongSoundManager.instance.GhostDistance(Mathf.Abs(transform.position.x - Character.instance.transform.position.x));
            if (transform.position.x < Character.instance.transform.position.x) {
                return 1;
			}
            return -1;
		}

        SongSoundManager.instance.GhostDistance(-1);

        if (Mathf.Abs(RoomManager.instance.currentRoom - ghostRoom) <= 1) {
            if (RoomManager.instance.currentRoom - ghostRoom < 0) {
                return -1;
			}
            return 1;
		}

        if (RoomManager.instance.currentRoom - ghostRoom < 0) {
            return 1;
        }
        return -1;
    }

    public void MoveGhostToRoom(Position direction) {
        ghostRoom = RoomManager.instance.GetNextRoomIndex(ghostRoom, direction);
        Vector3 ghostPos = transform.position;
        float xPos = RoomManager.instance.GetNextRoomGhostPosition(ghostRoom, direction);
        transform.position = new Vector3(xPos, ghostPos.y, ghostPos.z);
    }

    public void KillGhost() {
        rig.velocity = Vector2.zero;
        Destroy(this);
    }
}
