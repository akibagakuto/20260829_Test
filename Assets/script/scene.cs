using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMove : MonoBehaviour
{
    [SerializeField]
    private string NextSceneNeme;
    public void change_button()
    {
        SceneManager.LoadScene(NextSceneNeme);
    }
}