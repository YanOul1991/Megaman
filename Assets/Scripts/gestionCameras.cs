using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionCameras : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;

    // Start is called before the first frame update
    void Start()
    {
        // Active cam1 et desactive cam2
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Reappel la fonction de depart pour activer cam 1 et desactiver cam2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Start();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camera1.SetActive(false); 
            camera2.SetActive(true);
        }
    }
}
