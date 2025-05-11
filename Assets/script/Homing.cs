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
    public float attackDelay = 1.0f; // ���ߎ���
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
        if (isAttacking) return; // �U�����͓����Ȃ�

        float distanceToPlayer = Vector3.Distance(transform.position, playerTr.position);

        if (distanceToPlayer <= attackRange && isGrounded)
        {
            Debug.Log("�������I");
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
        targetPos.y = transform.position.y; // Y���̉�]�����ɂ������ꍇ�i������]�̂݁j

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

        // �v���C���[�Ƀq�b�g�������̒��˕Ԃ�
        if (!hasBounced && collision.gameObject.CompareTag("Player"))
        {
            // �_���[�W����
            Debug.Log("�v���C���[�Ƀq�b�g�I");
            GameObject DamageCal = GameObject.Find("GameObject");
            GameObject player = GameObject.Find("UnityChanSSU_DynCol");
            DamageCal damageCal = DamageCal.GetComponent<DamageCal>();
            damageCal.Damagecal(gameObject, 1f, player);

            // �����́i�t�����ɒ��˕Ԃ�j
            Vector3 bounceDir = (transform.position - playerTr.position).normalized + Vector3.up;
            rb.velocity = Vector3.zero; // ���O�̑��x�����Z�b�g
            rb.AddForce(bounceDir * 5f, ForceMode.Impulse);

            hasBounced = true;

            // ���ɖ߂������Ƃ�
            StartCoroutine(ResetAfterBounce());
        }
    }


    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // ���ߎ��ԁF�����k��
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.7f, originalScale.z * 1.2f);
        yield return new WaitForSeconds(attackDelay);

        // ���ɖ߂��ē���W�����v�I
        transform.localScale = originalScale;
        Vector3 dir = (playerTr.position - transform.position).normalized;
        Vector3 attackForce = dir * 3f + Vector3.up * 10f; // �������{�W�����v����
        rb.AddForce(attackForce, ForceMode.Impulse);

        hasBounced = false; // �������胊�Z�b�g
    }


    private IEnumerator ResetAfterBounce()
    {
        yield return new WaitForSeconds(0.5f); // ���������܂ŏ����҂�
        isAttacking = false;
        hopTimer = hopCooldown; // �ړ��ɖ߂邽�߂̃^�C�}�[���Z�b�g
    }

}
