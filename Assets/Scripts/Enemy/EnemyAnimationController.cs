using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAnimationController : MonoBehaviour
{
    // Animator del segundo personaje
    private Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();
        // Suscribirse al evento de animaci�n del primer personaje cuando el objeto est� habilitado
        PlayerAnimationController.OnMomentoClave += EjecutarAnimacion;

    }

    void OnDisable()
    {
        // Desuscribirse al evento cuando el objeto est� deshabilitado para evitar p�rdidas de memoria
        PlayerAnimationController.OnMomentoClave -= EjecutarAnimacion;
    }

    // M�todo para ejecutar la animaci�n en respuesta al evento de animaci�n del primer personaje
    private void EjecutarAnimacion()
    {
        Enemy enemy = GameManager.Instance.GetEnemy();
        CardScriptable newCard = DeckManager.Instance.lastPlayedCard;
        CardEffect c = newCard.cardEffects.Where((x) => { return x.type == CardType.DAMAGE; }).FirstOrDefault();

        if (c != null)
        {
            if (c.block != enemy.currentBlock)
            {
                // Activar la animaci�n del segundo personaje
                anim.SetTrigger("k_laugh");
            }
            else
            {
                //
            }
        }

    }









    }
