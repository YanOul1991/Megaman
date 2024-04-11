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

    // Variable float pour vitesse de deplacement lors des attaques
    public float vitesseMaximale;

    // Sons du jeu
    public AudioClip sonMort;

    // Variable bool permettant au joueur de pouvoir attaquer
    private bool peutAttaquer = true;

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

            // Gestion des sprites de repors et course
            if(Mathf.Abs(vitesseX) > 0.9f)
            {
                GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("marche", false);
            }

            // Gestion des attaques avec la touche espace
            if(Input.GetKeyDown(KeyCode.Space))
            {   
                // Si megaman peut attaquer et qu,il n'est pas en plein saut
                if(peutAttaquer && GetComponent<Animator>().GetBool("saut") == false)
                {
                    GetComponent<Animator>().SetBool("attaque", true);
                    peutAttaquer = false;

                    Invoke("PrepareAttaque", 0.5f);                                                         
                }
            }

            // Si l'animation d;attaque est en cours
            if(GetComponent<Animator>().GetBool("attaque") == true)
            {
                // On multiplie la vitesse du deplacement de megaman en fesaent en sorte qu'elle ne depasse pas la vitesse max
                if(Mathf.Abs(vitesseX) <= vitesseMaximale)
                {
                    vitesseX *= 2;
                }
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
        else
        {
            GetComponent<Animator>().SetBool("saut", true);
        }

        // Lorsque collision avec object avec comme tag ennemi
        if (collision.gameObject.tag == "Ennemi")
        {
            // Si megaman est en animation d'attaque
            if (GetComponent<Animator>().GetBool("attaque") == true)
            {
                // Animation de mort des ennemis
                collision.gameObject.GetComponent<Animator>().SetBool("mort", true);
                
                // Desactivation du collider de l'ennemi 
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                // L'ennemi est detruit
                Destroy(collision.gameObject, 0.9f);
            }

            // Si megaman se fait touche par un ennemi, mais pas en attaque
            if (GetComponent<Animator>().GetBool("attaque") == false)
            {
                // Valeur de parametre "mort" de l'animator devient true
                GetComponent<Animator>().SetBool("mort", true);

                // Audio de mort joue une fois
                GetComponent<AudioSource>().PlayOneShot(sonMort);

                // Relance le Jeu
                Invoke("RelancerJeu", 2f);
            }
        }
    }

    // Fonction qui permet a megaman de pouvoir attaquer de nouveau
    private void PrepareAttaque()
    {
        // On arrete l'animation d'attaque
        GetComponent<Animator>().SetBool("attaque", false);

        // On permet l'attaque
        peutAttaquer = true;
    }

    // Fonction qui relance la scene du jeu
    private void RelancerJeu()
    {
        SceneManager.LoadScene(0);
    }
}
