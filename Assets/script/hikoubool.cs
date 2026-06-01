using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hikoubool : MonoBehaviour
{
    public Animator anim;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            anim.SetBool("hikou", !anim.GetBool("hikou"));
        }
    }
}
