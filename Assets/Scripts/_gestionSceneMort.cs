using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script de gestion de la scene de de defaite / scene de mort, lorsque le joueeur perdera la partie
   Par : Yanis Oulmane
   Dernire modification : 01/05/2024
 */


public class _gestionSceneMort : MonoBehaviour
{
    public TextMeshProUGUI textePointage;
    public TextMeshProUGUI texteDecompte;
    int decompte = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke a chaque seconde la fonction qui gere le decompte
        InvokeRepeating("GestionDecompte", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Si le joueur appuit sur la barre d'espace le jeu recommence
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Appel fonction pour charger le niveau1
            Invoke("RecommencerJeu", 0f);
        }

        // Met a jour l'affichage du decompte et du pointage
        texteDecompte.text = "Ça recommence dans : " + decompte.ToString();
        textePointage.text = deplacementPersonnage.pointage.ToString() + " points";

        // Si le decompte atteint 0
        if (decompte == 0)
        {
            // On Recommence le jeu automatiquement
            RecommencerJeu();
        }

    }

    // Fonction qui redemare la scene de jeu niveau1
    void RecommencerJeu()
    {
        SceneManager.LoadScene(1);
    }

    // Fonction qui gere le decompte
    void GestionDecompte()
    {
        //decremente de 1 
        decompte -= 1;
    }
}
