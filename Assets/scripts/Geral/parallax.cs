using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class parallax : MonoBehaviour
{
    [Header("Principal")]
    [SerializeField, Range(0, 100), Tooltip("Velocidade exclusiva deste objeto em porcentagem (Em rela��o � c�mera)")] float localSpeed = 0;        // Velocidade espec�fica. (deixa zero para usar velocidade geral)
    [SerializeField, Range(1, 5), Tooltip("1 - Mais perto; 5 - Mais longe")] int layer = 1;                                                         // Layer do parallax.
    [SerializeField, Range(0.1f, 10), Tooltip("0.1 - Muita suaviza��o; 10 - suaviza��o desligada")] float smoothing = 10;                           // Suaviza��o do Parallax.
    [Header("Extras")]
    [SerializeField, Tooltip("A parallax afeta eixo Y?")] bool yParallax = false;                                                                   // Parallax afeta em y?
    [SerializeField, Tooltip("Ativar velocidade aleat�ria")] bool randomSpeed = false;                                                              // Velocidade aleat�ria.
    [SerializeField, Tooltip("Porcentagem do fato aleat�rio")] float randomFactor = 50;                                                             // Altera��o da velocidade inicial. (Porcentagem)

    // Essencial
    static float speed = 70;
    float s;
    float random = 0;
    Transform cam;
    [Header("Debug"), SerializeField] Vector3 startPos;

    void Start()
    {
        // Ajustes iniciais
        random = 1 + Random.Range(-randomFactor/2, randomFactor/2)/100;
        cam = Camera.main.transform;
        //startPos = transform.position/2;
        Debug.Log(random);
    }

    void Update()
    {
        float l = layer;
        // Ajustar configura��es.
        if(localSpeed == 0)
        {
            s = (speed * l/5)/100;
        }
        else
        {
            s = (localSpeed * l/5)/100;
        }
        if (randomSpeed)
        {
            s *= random;
            Mathf.Clamp01(s);
        }

        //Debug.Log(s);

        Vector3 parallax;
        if (yParallax)
        {
            parallax = new Vector3(startPos.x + (cam.position.x * s), startPos.y + (cam.position.y * s), transform.position.z);
        }
        else
        {
            parallax = new Vector3(startPos.x + (cam.position.x * s), transform.position.y, transform.position.z);
        }

        if(smoothing != 10)
        {
            transform.position = Vector3.Lerp(transform.position, parallax, Time.deltaTime * smoothing);
        }
        else
        {
            transform.position = parallax;
        }
    }
}
