using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    public static LevelClear levelClear;
    private int faseAtual = 1;
    private void Awake()
    {
        if(levelClear == null)
        {
            levelClear = this;
        }
        else if(levelClear != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LiberarFase(int FaseALiberar)
    {
        if(FaseALiberar > faseAtual)
        {
            faseAtual = FaseALiberar;

        }
    }
    public int GetFaseAtual()
    {
        return faseAtual;

    }
}
