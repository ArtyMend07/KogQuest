using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class Temporizador : MonoBehaviour
{

    public float tempoRestante;  // Declarada fora dos métodos
    public bool tempoCorrendo = false;
    public void Inicializa(float tempoInicial)
    {
        tempoRestante = tempoInicial;
        tempoCorrendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempoCorrendo && tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
        }
        else if (tempoRestante <= 0 && tempoCorrendo)
        {
            tempoCorrendo = false;
            tempoRestante = 0;
        }

    }
}
