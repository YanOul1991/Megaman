using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementCamera : MonoBehaviour
{
    // Variables publiques GameObject pour associer megaman et les cameras
    public GameObject cibleCamera;

    // variables publiques float pour les limites de la camera
    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Gestions de la position de la camera

        Vector3 positionCamera = transform.position;

        positionCamera = cibleCamera.transform.position;

        if (positionCamera.x < limiteGauche) positionCamera.x = limiteGauche;

        if (positionCamera.x > limiteDroite) positionCamera.x = limiteDroite;

        if (positionCamera.y < limiteBas) positionCamera.y = limiteBas;

        if (positionCamera.y > limiteHaut) positionCamera.y = limiteHaut;

        positionCamera.z = -10;

        transform.position = positionCamera;
    }
}
