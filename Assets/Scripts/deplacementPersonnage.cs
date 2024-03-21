using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deplacementPersonnage : MonoBehaviour
{
    private float vitesseX;
    public float vitesseDeplacement;
    private float vitesseY;
    public float forceSaut;
    
    // Boolean pour voir si la partie est termine
    //private bool partieTermine = false;

    // Sons
    public AudioClip sonMort;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On ecoute les touches du claviers tant que le personnage n'est pas mort
        if (GetComponent<Animator>().GetBool("mort") == false)
        {
            // vitesse en X
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseDeplacement;
               
                GetComponent<SpriteRenderer>().flipX = true; // Flip le sprite si le personnage cours a gauche
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                vitesseX = vitesseDeplacement;

                GetComponent<SpriteRenderer>().flipX = false; // Flip pas le sprite si le personnage cours a gauche
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
            }

            //Sauts
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Physics2D.OverlapCircle(transform.position, 0.25f))
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

        }
        // On applique les vitesses au personnage
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sort de l'animation de saut si megaman touche a un object avec ses pieds
        if (Physics2D.OverlapCircle(transform.position, 0.25f))
        {
            GetComponent<Animator>().SetBool("saut", false);
        }

        // Lorsque collision avec ennemi
        if (collision.gameObject.name == "wheeler")
        {
            // Valeur de condition `mort` de l'animator devient true
            GetComponent<Animator>().SetBool("mort", true);

            // Audio de mort joue une fois
            GetComponent<AudioSource>().PlayOneShot(sonMort);

            // On imobilise megaman a sa position x 
            vitesseX = 0;

            // Relance le Jeu
            Invoke("RelancerJeu", 2f);
        }
    }

    // Fonction qui va redemarer la scene du jeu
    private void RelancerJeu()
    {
        SceneManager.LoadScene(0);
    }
}
