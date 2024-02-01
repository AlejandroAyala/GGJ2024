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
        // Suscribirse al evento de animación del primer personaje cuando el objeto está habilitado
        PlayerAnimationController.OnMomentoClave += EjecutarAnimacion;

    }

    void OnDisable()
    {
        // Desuscribirse al evento cuando el objeto está deshabilitado para evitar pérdidas de memoria
        PlayerAnimationController.OnMomentoClave -= EjecutarAnimacion;
    }

    // Método para ejecutar la animación en respuesta al evento de animación del primer personaje
    private void EjecutarAnimacion()
    {
        Enemy enemy = GameManager.Instance.GetEnemy();
        CardScriptable newCard = DeckManager.Instance.lastPlayedCard;
        CardEffect c = newCard.cardEffects.Where((x) => { return x.type == CardType.DAMAGE; }).FirstOrDefault();

        if (c != null)
        {
            if (c.block != enemy.currentBlock)
            {
                // Activar la animación del segundo personaje
                anim.SetTrigger("k_laugh");
            }
            else
            {
                //
            }
        }

    }









    }
