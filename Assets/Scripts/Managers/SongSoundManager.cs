using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSoundManager : MonoBehaviour {

    public static SongSoundManager instance;

    public AudioSource ambient;
    public AudioSource heartbeat;
    public AudioSource chased;
    public AudioSource musicBox;

    public AudioSource doorClosed;
    public AudioSource openDoor;
    public AudioSource unlockDoor;

    public AudioSource walking;

    //For heartbeat speed calc
    float minPitch = .7f;
    float maxPitch = 3f;
    float minDist = 4;
    float maxDist = 50;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    public void StopAllSongs() {
        ambient.Stop();
        heartbeat.Stop();
        chased.Stop();
        musicBox.Stop();
    }

    public void GhostSpawned() {
        StopAllSongs();
        chased.pitch = 1;
        chased.Play();
        heartbeat.Play();
	}

    public void GhostDespawned() {
        StopAllSongs();
        ambient.Play();
    }

    public void GhostDistance(float distance) {
        float setPitchTo = 1;

        if (distance <= 0) {
            setPitchTo = 1;
        } else if (distance <= minDist) {
            setPitchTo = maxPitch;
		} else if (distance >= maxDist) {
            setPitchTo = minPitch;
		} else {
            float pitchDiff = maxPitch - minPitch;
            float distDiff = maxDist - minDist;

            pitchDiff *= (distance - minDist) / distDiff;
            setPitchTo = maxPitch - pitchDiff;
		}

        heartbeat.pitch = setPitchTo;
	}

    public void PlayThunder() {

	}

    public void DoorIsClosed() {
        doorClosed.Play();
    }

    public void OpenDoor() {
        openDoor.Play();
    }

    public void UnlockDoor() {
        unlockDoor.Play();
    }

    public void Walk() {

	}
}
