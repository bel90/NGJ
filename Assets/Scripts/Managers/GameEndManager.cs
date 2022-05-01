using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour {

	public static GameEndManager instance;

    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        Ghost.instance.KillGhost();
        Character.instance.KillPlayer();
	}

    public void Win() {
        gameWinPanel.SetActive(true);
        Ghost.instance.KillGhost();
        Character.instance.KillPlayer();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
