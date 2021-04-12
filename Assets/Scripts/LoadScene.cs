using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void LoadGameScene() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
