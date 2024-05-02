using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script de gestion des roues dentees 
 * Gestions des animation et components lorsque elles interagisent avec le joueur
   Par : Yanis Oulmane
   Derniere modification : 01/05/2024
 */


public class roueDente : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Megaman")
        {
            GetComponent<Animator>().enabled = true;

            GetComponent<CircleCollider2D>().enabled = false;

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            GetComponent<Rigidbody2D>().angularVelocity = 0;

            GetComponent<Rigidbody2D>().gravityScale = 0;

            Destroy(gameObject, 0.9f);
        }
    }
}
