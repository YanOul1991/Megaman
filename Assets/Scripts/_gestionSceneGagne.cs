using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _gestionSceneGagne : MonoBehaviour
{
    // Variable AudioClip qui sera asigne le son de victoire
    public AudioClip musique;
    public TextMeshProUGUI textePointage;
    public GameObject imageTrophe;

    // Start is called before the first frame update
    void Start()
    {
        // Si le pointage obtenu par le joueur est plus petit que le record
        if (deplacementPersonnage.pointage < deplacementPersonnage.pointageRecord)
        {
            // Il ne merite pas de trophee
            imageTrophe.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lorsque le joueur apuit sur espace on retourne a la scene introduction

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fonction qui retourne a la scene intro
            Invoke("RetournerIntro", 0);
        }

        // La fonction pour retourner a intro sera automatiquement invoque lorsque la musique de vivtoire aura finit de jouer
        Invoke("RetournerIntro", musique.length);

        // Affiche le texte avec le nombre de points
        textePointage.text = deplacementPersonnage.pointage.ToString() + " points!";
    }

    void RetournerIntro()
    {
        SceneManager.LoadScene(0);
    }
}
