using UnityEngine;
using TMPro; // TextMeshPro

public class UIObjetivoBotellas : MonoBehaviour
{
    public int botellasObjetivo = 0;
    public TextMeshProUGUI textoObjetivo; 

    void Start()
    {
        botellasObjetivo = Random.Range(3, 11);
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        textoObjetivo.text = "Procesa " + botellasObjetivo + " botellas para comenzar a puntuar.";
    }

    public void BotellaProcesada()
    {
        botellasObjetivo--;

        if (botellasObjetivo > 0)
        {
            ActualizarTexto();
        }
        else
        {
            textoObjetivo.text = "¡Puntaje activado!";
        }
    }
}
