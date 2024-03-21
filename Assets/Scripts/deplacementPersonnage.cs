using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deplacementPersonnage : MonoBehaviour
{
    private float vitesseX;
    public float vitesseCourse;
    private float vitesseY;
    public float forceSaut;

    // Sons du jeu
    public AudioClip sonMort;

    // Update is called once per frame
    void Update()
    {
        // On ecoute les touches du clavier si le parametre "mort" de megaman est faux
        if (GetComponent<Animator>().GetBool("mort") == false)
        {
            // vitesse en X
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseCourse;
                // Flip X est true si deplacement vers la gauche
                GetComponent<SpriteRenderer>().flipX = true; 
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                vitesseX = vitesseCourse;
                // Flip X est false deplacement vers la droite
                GetComponent<SpriteRenderer>().flipX = false; 
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
            }

            //Sauts
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Physics2D.OverlapCircle(transform.position, 0.2f))
            {
                vitesseY = forceSaut;

                GetComponent<Animator>().SetBool("saut", true);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }

            // Gestion des sprites de report et course
            if(Mathf.Abs(vitesseX) > 0.9f)
            {
                GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("marche", false);
            }

            // On applique les vitesses au personnage
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sort de l'animation de saut si megaman touche a un object avec ses pieds
        if (Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            GetComponent<Animator>().SetBool("saut", false);
        }

        // Lorsque collision avec ennemi
        if (collision.gameObject.name == "wheeler")
        {
            // Valeur de parametre "mort" de l'animator devient true
            GetComponent<Animator>().SetBool("mort", true);

            // Audio de mort joue une fois
            GetComponent<AudioSource>().PlayOneShot(sonMort);

            // Relance le Jeu
            Invoke("RelancerJeu", 2f);
        }
    }

    // Fonction qui relance la scene du jeu
    private void RelancerJeu()
    {
        SceneManager.LoadScene(0);
    }
}
