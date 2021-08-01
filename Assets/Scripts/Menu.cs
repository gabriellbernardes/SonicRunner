 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void CarregarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CarregarFase( int fase)
    {
        if(fase <= LevelClear.levelClear.GetFaseAtual())
        {
            SceneManager.LoadScene(fase);
        }
        else
        {
            return;
        }
    }
}
