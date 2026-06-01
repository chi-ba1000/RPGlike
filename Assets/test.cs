using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform testobj;
    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objposition = (testobj.position);
        Debug.Log(objposition);


        float Horizontal = (Input.GetAxis("Horizontal"));
        Debug.Log(Horizontal);

        float Vertical = (Input.GetAxis("Vertical"));
        Debug.Log(Vertical);

        Vector3 pos = new Vector3(objposition.x + Horizontal, objposition.y, objposition.z + Vertical);
        testobj.position = pos;


    }
}