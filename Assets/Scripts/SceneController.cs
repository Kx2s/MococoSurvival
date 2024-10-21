using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void moveScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}