using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la balle entre en collision avec un ennemi ou le decor
        if (collision.gameObject.tag == "Ennemi" || collision.gameObject.tag == "plateforme" || collision.gameObject.tag == "boite")
        {
            // Activation de l'animation de l'explosion
            GetComponent<Animator>().enabled = true;

            // Detruit la balle apres l'animation
            Destroy(gameObject, 0.15f);

            // si collision avec un ennemi et qui n'est pas une abeille
            if (collision.gameObject.tag == "Ennemi" && collision.gameObject.name != "Abeille")
            {
                //Activation de l'animation d'explosion
                collision.gameObject.GetComponent<Animator>().enabled = true;

                // Destruction de l'objet apres son animation
                Destroy(collision.gameObject, 0.5f);

                // Enleve la gravite, la volocite et le angularVelocity de la roue
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, 0, 0);
                collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }

            // si collision avec une abeille
            if (collision.gameObject.name == "Abeille")
            {
                // Set trigger de l'animation de mort
                collision.gameObject.GetComponent<Animator>().SetTrigger("mort");

                // Desactivation du capsuleCollider
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                // L'ennemi est detruit apres son animation
                Destroy(collision.gameObject, 0.9f);
            }
        }
    }
}
