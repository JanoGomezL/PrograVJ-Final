using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    // Amplitud del movimiento (30 cm)
    public float amplitude = 0.3f; // 30 cm en unidades de Unity
    // Frecuencia del movimiento (velocidad)
    public float frequency = 3f; // Aumentar la frecuencia para mayor velocidad

    // Posición inicial del objeto
    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posición inicial
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcular el desplazamiento
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        // Actualizar la posición del objeto
        transform.position = startPosition + new Vector3(offset, 0, 0);
    }
}
