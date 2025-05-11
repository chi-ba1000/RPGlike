using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Homing : MonoBehaviour
{
    Transform playerTr;
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float hopCooldown = 1.5f;
    public float HP = 1f;

    public float attackRange = 2f;
    public float attackDelay = 1.0f; // ため時間
    public float attackDamage = 1.0f;
    private bool isAttacking = false;
    private bool hasBounced = false;


    public AttackArea attackArea;
    public Attack attack;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float hopTimer = 0f;

    private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        hopTimer = hopCooldown;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isAttacking) return; // 攻撃中は動かない

        float distanceToPlayer = Vector3.Distance(transform.position, playerTr.position);

        if (distanceToPlayer <= attackRange && isGrounded)
        {
            Debug.Log("ちかい！");
            StartCoroutine(AttackPlayer());
            return;
        }

        hopTimer -= Time.deltaTime;

        if (isGrounded && hopTimer <= 0f)
        {
            StartCoroutine(HopWithSquash());
            hopTimer = hopCooldown;
        }

        if (gameObject.transform.position.y < 20f)
        {
            Destroy(gameObject);
        }
        Vector3 targetPos = playerTr.position;
        targetPos.y = transform.position.y; // Y軸の回転だけにしたい場合（水平回転のみ）

        transform.LookAt(targetPos);

    }

    private IEnumerator HopWithSquash()
    {
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.8f, originalScale.z * 1.2f);
        yield return new WaitForSeconds(0.1f);

        transform.localScale = originalScale;

        Vector3 direction = (playerTr.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;

        Vector3 force = direction * moveSpeed + Vector3.up * jumpForce;
        rb.AddForce(force, ForceMode.Impulse);
        isGrounded = false;
    }

    private IEnumerator LandSquash()
    {
        transform.localScale = new Vector3(originalScale.x * 1.3f, originalScale.y * 0.6f, originalScale.z * 1.3f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            StartCoroutine(LandSquash());
        }

        // プレイヤーにヒットした時の跳ね返り
        if (!hasBounced && collision.gameObject.CompareTag("Player"))
        {
            // ダメージ処理
            Debug.Log("プレイヤーにヒット！");
            GameObject DamageCal = GameObject.Find("GameObject");
            GameObject player = GameObject.Find("UnityChanSSU_DynCol");
            DamageCal damageCal = DamageCal.GetComponent<DamageCal>();
            damageCal.Damagecal(gameObject, 1f, player);

            // 反発力（逆方向に跳ね返る）
            Vector3 bounceDir = (transform.position - playerTr.position).normalized + Vector3.up;
            rb.velocity = Vector3.zero; // 直前の速度をリセット
            rb.AddForce(bounceDir * 5f, ForceMode.Impulse);

            hasBounced = true;

            // 元に戻す処理とか
            StartCoroutine(ResetAfterBounce());
        }
    }


    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // ため時間：少し縮む
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.7f, originalScale.z * 1.2f);
        yield return new WaitForSeconds(attackDelay);

        // 元に戻して特大ジャンプ！
        transform.localScale = originalScale;
        Vector3 dir = (playerTr.position - transform.position).normalized;
        Vector3 attackForce = dir * 3f + Vector3.up * 10f; // 横方向＋ジャンプ強め
        rb.AddForce(attackForce, ForceMode.Impulse);

        hasBounced = false; // 反発判定リセット
    }


    private IEnumerator ResetAfterBounce()
    {
        yield return new WaitForSeconds(0.5f); // 落ち着くまで少し待つ
        isAttacking = false;
        hopTimer = hopCooldown; // 移動に戻るためのタイマーリセット
    }

}
