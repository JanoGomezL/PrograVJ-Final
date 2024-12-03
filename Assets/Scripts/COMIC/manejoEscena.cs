using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI displayText; // Referencia al Text (TMP) en la escena
    public Image displayImage;         // Referencia a la Image en la escena
    public Button nextButton;          // Botón de siguiente
    public Button backButton;          // Botón de atrás
    public GameObject startButtonPanel; // Panel que contiene el botón "COMENZAR"

    [Header("Prefabs")]
    public List<string> texts;         // Lista de textos
    public List<Sprite> images;        // Lista de imágenes

    [Header("Typewriting Effect")]
    public float typingSpeed = 0.02f;  // Velocidad del efecto de escritura (más rápido)

    private int currentIndex = 0;      // Índice actual del paso
    private Coroutine typingCoroutine; // Referencia a la corrutina activa

    void Start()
    {
        // Configurar botones
        nextButton.onClick.AddListener(NextStep);
        backButton.onClick.AddListener(PreviousStep);

        if (startButtonPanel != null)
        {
            // Ocultar el panel del botón "COMENZAR" al inicio
            startButtonPanel.SetActive(false);
        }

        // Inicializar la primera pantalla
        UpdateUI();
    }

    void UpdateUI()
    {
        // Cambiar la imagen según el índice actual
        displayImage.sprite = images[currentIndex];

        // Detener cualquier corrutina activa y empezar la escritura del texto
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(texts[currentIndex]));

        // Activar/desactivar botones según el índice
        backButton.interactable = currentIndex > 0;                        // Desactivar si es el primer paso
        nextButton.interactable = currentIndex < texts.Count - 1;          // Desactivar si es el último paso
    }

    IEnumerator TypeText(string text)
    {
        displayText.text = ""; // Vaciar el texto actual
        foreach (char letter in text.ToCharArray())
        {
            displayText.text += letter; // Agregar letra por letra
            yield return new WaitForSeconds(typingSpeed); // Esperar antes de agregar la siguiente
        }

        // Si el texto es el último de la lista, mostrar el botón "COMENZAR"
        if (currentIndex == texts.Count - 1 && startButtonPanel != null)
        {
            startButtonPanel.SetActive(true); // Mostrar el panel con el botón
            Button startButton = startButtonPanel.GetComponentInChildren<Button>();
            if (startButton != null)
            {
                startButton.onClick.AddListener(StartGame);
            }
        }
    }

    public void NextStep()
    {
        if (currentIndex < texts.Count - 1)
        {
            currentIndex++;
            UpdateUI();

            // Ocultar el panel del botón "COMENZAR" en caso de retroceder antes de la última escena
            if (startButtonPanel != null)
            {
                startButtonPanel.SetActive(false);
            }
        }
    }

    public void PreviousStep()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();

            // Ocultar el panel del botón "COMENZAR" en caso de retroceder antes de la última escena
            if (startButtonPanel != null)
            {
                startButtonPanel.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        // Cambiar a la escena "PruebaMapa"
        SceneManager.LoadScene("PruebaMapa");
    }
}
