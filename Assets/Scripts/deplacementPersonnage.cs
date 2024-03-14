using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementPersonnage : MonoBehaviour
{
    private float vitesseX;
    public float maxVitesseX;
    private float vitesseY;
    public float forceSaut;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // vitesse en X
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vitesseX = -maxVitesseX;
               
            GetComponent<SpriteRenderer>().flipX = true; // Flip le sprite si le personnage cours a gauche
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            vitesseX = maxVitesseX;

            GetComponent<SpriteRenderer>().flipX = false; // Flip pas le sprite si le personnage cours a gauche
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;
        }

        //Sauts
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("saut", false);
    }
}
