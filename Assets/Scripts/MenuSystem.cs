using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{

     public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
   
    public void controles ()
    {
        SceneManager.LoadScene("Controles");
    }   


    public void Salir()
    {
       Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}