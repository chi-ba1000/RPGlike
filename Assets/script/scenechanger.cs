using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//ƒƒCƒ“ƒƒjƒ…[‘JˆÚ
public class scenechanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void Gomain()
    {
        SceneManager.LoadScene("main");
    }

    public void Gotitle()
    {
        SceneManager.LoadScene("title");
    }
}
