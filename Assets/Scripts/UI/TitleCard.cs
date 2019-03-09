using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour
{
    //[SerializeField]
    //private Button StartButton;
    
    public void OnClickOnStart()
    {
        SceneManager.LoadScene("Main");
    }
}
