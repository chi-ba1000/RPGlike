using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.U2D; // NavMeshAgentを使うために必要

public class EnemyController : MonoBehaviour
{
    public Transform player;
    [Tooltip("敵の攻撃距離")]
    public float attackdistance;
    [Tooltip("敵の回転速度")]
    public float rotationSpeed;
    [Tooltip("敵の索敵距離")]
    public float searchdistance;
    [Tooltip("敵の目の位置")]
    public Vector3 eyeposition;
    [Tooltip("敵の移動の中心点")]
    public Vector3 centerpoint;
    [Header("ランダムウォークの範囲")]
    public float XRange;
    public float YRange;
    public float ZRange;
    [Tooltip("待機時間")]
    public float waitSec;
    [Tooltip("スタックしているとみなす時間")]
    public int stackSec;
    public float MaxHP;
    public float Armor = 1f;
    public float currentHP;
    public bool isDeathspawn;
    public Enemytype enemytype;
    public bool candroppeditem;
    public GameObject itemPrefab;
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数
    private Animator anim;
    private Vector3 position;
    private bool isSprint;
    private int fixedCount;
    private int stackCounter;
    private bool isDeath = false;
    public enemyHPUI enemyHPUI;
    public fisherman fisherman;
    public enum Enemytype{zako,midboss, boss,}

    void Start()
    {
        // 初期化処理
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHP = MaxHP;
        enemyHPUI = GetComponentInChildren<enemyHPUI>();
        enemyHPUI.EnemyUpdateHPUI(currentHP, MaxHP);
        RandomDest();
    }

    void Update()
    {
        //キャラクターの目からプレイヤーまでの距離を計算
        float distance = Vector3.Distance(transform.position + eyeposition, player.position + new Vector3(0f, 1.2f, 0f));
        Vector3 eyePosition = transform.position + eyeposition;
        Vector3 playerCenter = player.position + Vector3.up * 1.0f;
        Vector3 directionToPlayer = (playerCenter - eyePosition).normalized;

        //距離がattackdistance以下の時
        if (distance < attackdistance)
        {
            agent.isStopped = true;
            RaycastHit hit;
            //プレイヤーに向けてRayを出してそれがプレイヤーに当たったとき
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance) && hit.collider.CompareTag("Player") && !isDeath)
            {
                enemyattackmotion enemyattackmotion = gameObject.GetComponent<enemyattackmotion>();
                //敵の種類によって攻撃モーションを変える
                switch (enemytype)
                {
                    case Enemytype.zako:
                        enemyattackmotion.Zako();
                        break;
                    case Enemytype.midboss:
                        enemyattackmotion.Midboss();
                        break;
                    case Enemytype.boss:
                        // ボスの攻撃モーションを呼び出す
                        enemyattackmotion.Boss();
                        break;
                }
            }

        }

        //距離がsearchdistance以下の時
        if (distance < searchdistance)
        {
            agent.isStopped = false;
            RaycastHit hit;
            bool isCanseePlayer = false;
            //プレイヤーに向けてRayを出してそれがプレイヤーに当たったとき
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance) && hit.collider.CompareTag("Player"))
            {
                isCanseePlayer = true;
            }
            //プレイヤーが見えたとき目的地をプレイヤーにセット
            if (isCanseePlayer)
            {
                agent.SetDestination(player.position);
                anim.SetBool("walk", true);
                anim.SetBool("idle", false);
                Vector3 RandomPosition = centerpoint + player.position;
                NavMeshHit playerhit;
                if (NavMesh.SamplePosition(RandomPosition, out playerhit, YRange, agent.areaMask))
                {
                    agent.SetDestination(playerhit.position);
                    Debug.Log($"NewDestenation{playerhit.position}");
                }
                Debug.Log("detect");
            }
        }

        else
        {
            //目的地に着いたとき次の目的地をセットする
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                StartCoroutine(WaitforNewDest());
                Debug.Log("wait");

            }
            //デスポーンするときの処理
            if (isDeathspawn)
            {
                Debug.Log("デスポーン");
                StartCoroutine(Destory());
            }

        }

    }

    //スタック処理
    private void FixedUpdate()
    {
        fixedCount++;
        Vector3 currentposition = transform.position;
        //stackSecの間スタックした場合目的地を更新する
        if (fixedCount % 50 == 0 && currentposition == position)
        {
            stackCounter++;
            Debug.Log("stackked");
        }
        if (stackCounter == stackSec)
        {
            RandomDest();
            stackCounter = 0;
        }
        position = currentposition;
    }

    //デバック用
    void OnDrawGizmosSelected()
    {
        // 索敵範囲の円
        Gizmos.color = Color.yellow;
        Vector3 patrolBoxSize = new Vector3(XRange * 2, YRange * 2, ZRange * 2);
        Gizmos.DrawWireCube(centerpoint, patrolBoxSize);
        Gizmos.DrawWireSphere(transform.position, searchdistance);

        // 視線のデバッグ表示
        if (player != null)
        {
            Vector3 eyePosition = transform.position + eyeposition;
            Vector3 playerCenter = player.position + Vector3.up * 1.0f;
            Vector3 directionToPlayer = (playerCenter - eyePosition).normalized;

            RaycastHit hit;
            // Rayを飛ばしてみて、何に当たったかで色を変える
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // プレイヤーに届いている（緑）
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(eyePosition, hit.point);
                }
                else
                {
                    // 壁に遮られている（赤）
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(eyePosition, hit.point);
                }
            }
            else
            {
                // 誰にも当たらない（グレー）
                Gizmos.color = Color.gray;
                Gizmos.DrawRay(eyePosition, directionToPlayer * searchdistance);
            }
        }
    }

    //音に反応する敵の時プレイヤーから発火できるようにして，目的地をプレイヤーの座標へセット
    public void Makenoise()
    {
        agent.SetDestination(player.position);
    }

    //範囲内のランダムな座標を指定する
    void RandomDest()
    {
        anim.SetBool("walk", true);
        anim.SetBool("idle", false);
        float randomX = Random.Range(-XRange, XRange);
        float randomY = Random.Range(-YRange, YRange);
        float randomZ = Random.Range(-ZRange, ZRange);
        Vector3 RandomPosition = centerpoint + new Vector3(randomX, randomY, randomZ);
        NavMeshHit hit;
        //ランダムな座標がNavMesh上にあるか確認し，NavMeshの属性の目的地をセットする
        if (NavMesh.SamplePosition(RandomPosition, out hit, YRange, agent.areaMask))
        {
            agent.SetDestination(hit.position);
            Debug.Log($"NewDestenation{hit.position}");
        }

    }

    //ダメージを受けるときの処理
    public void TakeDamage(float Damage)
    {
        anim.SetTrigger("hit");
        Debug.Log(gameObject.name + "に" + Damage);
        currentHP = currentHP - Damage * Armor;
        //ダメージをHPUIに送り更新する
        enemyHPUI.EnemyUpdateHPUI(currentHP, MaxHP);
        if (currentHP <= 0)
        {
            //死亡モーションを呼び出す
            anim.SetTrigger("die");
            StartCoroutine(Destory());
        }
    }

    //目的地に着いた後次の移動をするまでの待機時間
    IEnumerator WaitforNewDest()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        float randomSec = Random.Range(waitSec, waitSec + 2);
        yield return new WaitForSeconds(randomSec);
        RandomDest();
        Debug.Log("waited");
    }


    IEnumerator Destory()
    {
        isDeath = true;
        yield return new WaitForSeconds(1.5f);
        //アイテムドロップの処理
        if (candroppeditem)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        //bossを倒したときの処理
        if (enemytype == Enemytype.boss)
        {
            fisherman.missionnum = 3;
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //敵が飛行するようになるトリガー
        if (other.gameObject.CompareTag("motion"))
        {
            anim.SetBool("hikou", !anim.GetBool("hikou"));
        }
    }

}