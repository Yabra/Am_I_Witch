using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void Learning()
    {
        SceneManager.LoadScene("Learning");
    }
}
