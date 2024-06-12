using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{
   public void yenidenBasla()
    {
        SceneManager.LoadScene("gameLevel");
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("menuLevel");
    }
    
}
