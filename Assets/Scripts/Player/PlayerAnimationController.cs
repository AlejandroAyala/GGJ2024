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

    // M�todo que ser� llamado desde el evento de animaci�n
    public void OnEventoDeAnimacion()
    {
        // Aqu� puedes poner la l�gica que deseas ejecutar en respuesta al evento de animaci�n
        Debug.Log("Evento de animaci�n ejecutado en el primer personaje.");
        OnMomentoClave.Invoke();
    }
}