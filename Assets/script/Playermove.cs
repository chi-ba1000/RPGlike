using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    public float jumpForce = 10f; // ジャンプの力
    public float gravityDelay = 0.5f; // 重力を復帰させるまでの時間
    public float RunSpeed = 2f;
    public float WalkSpeed = 1f;
    public Transform Neck;
    public Transform Aimpos;

    private Rigidbody rb;
    private Animator anim;
    private bool isGround;
    private bool JumpPlay;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float Speed = Mathf.Max(Mathf.Abs(v), Mathf.Abs(h));
        float CurrentSpeed = 1f * WalkSpeed;
        bool current = anim.GetBool("Run");
        Debug.Log(current);
        if (Speed <= 1f && !current)
        {
            anim.SetFloat("WalkSpeed", WalkSpeed * 0.3f);
        }
        else if (current == true)
        {
            CurrentSpeed = 1.5f * RunSpeed;
            anim.SetFloat("RunSpeed", RunSpeed*0.15f);
        }
        else
        {
            anim.speed = 1;
        }


        anim.SetFloat("Speed", Speed);
        
        


        velocity = new Vector3(0, 0, Speed * 4);
        velocity = transform.TransformDirection(velocity) * CurrentSpeed;
        rb.AddForce(velocity, ForceMode.Impulse);
        //transform.localPosition += velocity * Time.fixedDeltaTime;


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Run", !current);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            StartCoroutine(SlideFalse());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }


        if (Speed <= 0.8f)
        {
            anim.SetBool("Run", false);
        }

        //Vector3 targetPos = Aimpos.position;
        //targetPos.x = Neck.position.x; // Y軸の回転だけにしたい場合（水平回転のみ）

        //transform.LookAt(targetPos);


    }
    IEnumerator SlideFalse()
    {
        anim.SetBool("Slide", true);
        // 1秒待機
        yield return new WaitForSeconds(1f);
        anim.SetBool("Slide", false);
    }

    IEnumerator Jump()
    {
        if (isGround && !JumpPlay)
        {
            Debug.Log("ジャンプ");
            bool current = anim.GetBool("Run");
            if (current)
            {
                isGround = false;
                Debug.Log("走り飛び");
                JumpPlay = true;
                anim.speed = 0.1f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                JumpPlay = false;
            }
            else
            {
                isGround = false;
                JumpPlay = true;
                anim.SetBool("Jump", true);
                yield return new WaitForSeconds(0.4f);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.speed = 0.5f;
                JumpPlay = false;
            }
        }
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面と接触を確認
        {
            Debug.Log("接地");

            isGround = true;
        }
    Debug.Log("こらーだー");

    isGround = true;

    anim.speed = 1;
    anim.SetBool("Jump", false);
    JumpPlay = false;
    }
}
