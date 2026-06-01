using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fisherman : MonoBehaviour
{
    public GameObject messageset;
    public TextMeshProUGUI maintext;
    public PlayerHPController playerHPController;
    public scenechanger scenechanger;
    public float healAmount;
    public int eventflag; //会話番号
    public int missionnum;//イベントフェーズ番号
    // Start is called before the first frame update
    void Start()
    {
        messageset.SetActive(false);
    }

    //UIオープン
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            // You can add more logic here, such as starting a dialogue or giving the player an item
            messageset.SetActive(true);
            StartCoroutine(HanashiStart());
        }
    }

    //UIクローズ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger");
            // You can add more logic here, such as ending a dialogue or removing an item from the player
            maintext.text = "";
            messageset.SetActive(false);
        }
    }

    //会話発火
    IEnumerator HanashiStart()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        StartCoroutine(Sendmessage());
    }
    IEnumerator Sendmessage()
    {
        //フェーズ１
        if (messageset.activeSelf && missionnum == 0)
        {
            switch (eventflag)
            {
                case 0:
                    maintext.text = "やあ、旅人さん。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 1:
                    maintext.text = "最近この辺で魚が釣れなくてねぇ。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 2:
                    maintext.text = "しかも、滝の上の方から変な鳴き声がするんだ。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 3:
                    maintext.text = "旅人さん。\n上に行って様子を見てきてくれないかね。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 4:
                    maintext.text = "そうそう、こんなものを釣ったから持っていくといい。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    //ここでアイテムを渡す処理を書く
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 5:
                    maintext.text = "あとは、けがをしたときは私のところに来てくれ。私の弁当を分けてやるから。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 6:
                    maintext.text = "それじゃあ、頼んだよ。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag = 0;
                    missionnum++;
                    messageset.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        //フェーズ２
        else if (messageset.activeSelf && missionnum == 1)
        {
            switch (eventflag)
            {
                case 0:
                    maintext.text = "どうだった？\n滝の上の方は？";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 1:
                    maintext.text = "ああ、そうかい。\nそれは大変だったねぇ。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 2:
                    maintext.text = "まあ、あまり無理をしないでね。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 3:
                    maintext.text = "そうそう、お弁当を持ってきたから、これを食べて元気を出してね。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    playerHPController.Heal(healAmount);
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag = 0;
                    missionnum++;
                    messageset.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        //フェーズ３
        else if (messageset.activeSelf && missionnum == 2)
        {
            switch (eventflag)
            {
                case 0:
                    maintext.text = "まだほしいのかい？";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    playerHPController.Heal(healAmount);
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    messageset.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        //フェーズ４
        else if (missionnum == 3)
        {
            switch (eventflag)
            {
                case 0:
                    maintext.text = "ああ、ありがとう。\nこれで安心して暮らせるよ。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag++;
                    StartCoroutine(Sendmessage());
                    break;
                case 1:
                    maintext.text = "それじゃあ、またね。";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    eventflag = 0;
                    messageset.SetActive(false);
                    break;
                case 2:
                    maintext.text = "おしまい";
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.F));
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    scenechanger.Gotitle();
                    break;
                default:
                    break;
            }
        }
    }
}
