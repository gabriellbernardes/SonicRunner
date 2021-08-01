using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    private int moedasAtual = 0;
    private bool gameOver = false;
    private float segundos;
    private int segundosToInt;
    private int minutos;

    public Text minutosText;
    public Text segundosText;
    public Text moedasText;

    public GameObject gameOverText;
    private bool passouDeFase = false;
    public GameObject passouDeFaseText;
    public int faseALiberar;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(levelManager == null)
        {
            levelManager = this;
        }
        else if(levelManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !passouDeFase)
        {
            segundos += Time.deltaTime;
            if(segundos >= 60)
            {
                minutos++;
                minutosText.text = minutos.ToString();
                segundos = 0;
            }
            segundosToInt = (int)segundos;

            if (segundosToInt < 10)
            {
                segundosText.text ="0" + segundosToInt.ToString();

            }
            else
            {
                segundosText.text = segundosToInt.ToString();

            }

        }
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(passouDeFase && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void setMoedas()
    {
        moedasText.text = moedasAtual.ToString();
        moedasAtual += 1;

    }
    public int getMoedas()
    {
        return moedasAtual; 

    }
    public void ResetMoedas()
    {
        moedasAtual = 0;
        moedasText.text = moedasAtual.ToString();
    }
    public void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
    }
    public void PassouDeFase()
    {

        passouDeFase = true;
        LevelClear.levelClear.LiberarFase(faseALiberar);
        passouDeFaseText.SetActive(true);

    }
}
