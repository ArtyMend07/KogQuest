using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
  public void Jogar_jogo()
{
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

}

public void Sair_jogo(){

    Application.Quit();
}
}
