using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public delegate void PlayAnimation();
    public static PlayAnimation OnMomentoClave;

    private Animator anim;
    void Start()
    {
        // Obtener el componente Animator
        anim = GetComponent<Animator>();
    }

    // Método que será llamado desde el evento de animación
    public void OnEventoDeAnimacion()
    {
        // Aquí puedes poner la lógica que deseas ejecutar en respuesta al evento de animación
        Debug.Log("Evento de animación ejecutado en el primer personaje.");
        OnMomentoClave.Invoke();
    }
}