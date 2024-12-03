using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Esta función se llama desde el botón.
    public void Exit()
    {
        // Mensaje para el editor (solo para pruebas).
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicación.
        Application.Quit();
    }
}
