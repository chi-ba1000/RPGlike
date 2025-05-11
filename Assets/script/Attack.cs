using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    public float leftclick;
    public Animator anim;
    public GameObject AttackArea;
    private bool cooltime = false; 
    private string hasitem = "none";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftclick = Input.GetAxis("Fire");
        Debug.Log(leftclick);
        int random = Random.Range(0, 2);
        if (leftclick == 0)
        {
            anim.SetBool("Attack", false);
        }
        if (leftclick > 0)
        {
            anim.SetInteger("random", random);
            anim.SetBool("Attack", true);
            if (cooltime == false)
            {
                StartCoroutine(AttackFrame());
            }
        }
    }
    IEnumerator AttackFrame()
    {
        if (hasitem == "none")
        {
            cooltime = true;
            yield return new WaitForSeconds(0.3f);
            yield return null;
            AttackArea.SetActive(true);
            yield return null;
            AttackArea.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            cooltime = false;
        }
    }
}
