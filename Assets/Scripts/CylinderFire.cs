using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderFire : MonoBehaviour
{
    GameObject cylinder;
    Material mat;
    Camera camera;

    GameObject sparkle;


    void Start()
    {
        camera = Camera.main;
        cylinder = GameObject.Find("Cylinder");
        mat= cylinder.GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Fire", camera.transform.position.y);

        sparkle = GameObject.Find("Sparkle");
        
    }

    private void Update()
    { 
        mat.SetFloat("_Fire", camera.transform.position.y-2);
        sparkle.transform.position = new Vector3(sparkle.transform.position.x, camera.transform.position.y-2, sparkle.transform.position.z);
    }
}
