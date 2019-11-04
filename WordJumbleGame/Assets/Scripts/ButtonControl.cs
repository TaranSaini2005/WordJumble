using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{   
    [SerializeField]
    private string scene;

    public void ChangeScene()
    {
        AudioManager.audioManager.PlaySound("Menu Navigation");
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
