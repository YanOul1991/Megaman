using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
/* Script de gestion de la scene de d'introduction
   Par : Yanis Oulmane
   Dernire modification : 01/05/2024
 */


public class _CommencerPartie : MonoBehaviour
{
    // Variable TMpro du texte qui va clignoter
    public TextMeshProUGUI texteClignote;
    public TextMeshProUGUI texteRecordPoints;

    // Variable couleur texte alpha pour recevoir les valeur de couleur du texte
    Color alphaTexte;

    // Variable int compteurClignot
    int compteurClignote = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Applique la valeur de couleur du texte a la variable alphaTexte
        alphaTexte = texteClignote.color;

        InvokeRepeating("ClignoterTexte", 0.1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Si le joueur appuit sur la barre d'espace le jeu commence
        if (Input.GetKeyDown(KeyCode.Space)) Invoke("CommencerJeu", 0f);

        // Met a jour les valeurs rgba du texte
        texteClignote.color = alphaTexte;

        texteRecordPoints.text = "Pointage à battre : " + deplacementPersonnage.pointageRecord;
    }

    // Fonction qui commence le jeu
    void CommencerJeu()
    {
        SceneManager.LoadScene(1);
    }

    // Fonction qui gere le clignotement du texte d'instruction
    void ClignoterTexte()
    {
        // Si le compteur de clignotement est inferieur a 6
        if (compteurClignote < 6)
        {
            // Variation de la couche alpha du texte d'instructions
            if (texteClignote.color.a == 1)
            {
                alphaTexte.a = 0;
            }

            if (texteClignote.color.a == 0) 
            {
                alphaTexte.a = 1;
            }
        }
        // Augmentation du compteur
        compteurClignote += 1;

        //  on repart le compteur a 0
        if (compteurClignote == 10)
        {
            compteurClignote = 0;
        }
    }
}
