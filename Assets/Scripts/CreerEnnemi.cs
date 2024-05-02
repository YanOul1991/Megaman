using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script de gestion de la creation d'ennemis par programmation avec un objet dynamique
   Gestion de la creation d'instances d'un ennemi avec la fonction Instantiate() et gestion de chaque nouvelle iteratrion de l'objet
   Par : Yanis Oulmane
   Dernire modification : 01/05/2024
 */

public class CreerEnnemi : MonoBehaviour
{
    public GameObject ennemiACreer;
    public GameObject personnage;

    // Variables float pour les limites a gauche et a droite
    public float limiteGauche;
    public float limiteDroite;

    // Start is called before the first frame update
    void Start()
    {
        // Appel de la fonction dupliquer roue a chaque 3sec
        InvokeRepeating("DupliquerRoue", 0, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fonction qui duplique la roue
    void DupliquerRoue()
    {
        // Si Megaman est dans les limites gauche et droite
        if(personnage.transform.position.x > limiteGauche && personnage.transform.position.x < limiteDroite)
        {
            // Dupliquation de l'ennemi
            GameObject cloneEnnemi = Instantiate(ennemiACreer);
            // Activation du clone
            cloneEnnemi.SetActive(true);
            // Positionne le clone a une position aleatoire entre -8f et 8f par rapport a megaman en X
            // position y fixe a 8f
            float positionX = Random.Range(personnage.transform.position.x - 8f, personnage.transform.position.x + 8f);
            cloneEnnemi.GetComponent<Transform>().position = new Vector3(positionX, 8f, 0);

        }
    }
}
