using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Esta funci�n se llama desde el bot�n.
    public void Exit()
    {
        // Mensaje para el editor (solo para pruebas).
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicaci�n.
        Application.Quit();
    }
}
