using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] items none;
    [SerializeField] items woodensword;
    [SerializeField] items ironsword;
    [SerializeField] Item currentItem;
    private Animator animator;
    public enum Item
    {
        none,woodensword,ironsword,
    }
    // Start is called before the first frame update
    void Start()
    {
        // ‰Šú‰»ˆ—
        currentItem = Item.none;
    }

    //ƒAƒCƒeƒ€‚ğE‚¤ˆ— 
    public string HasItem
    {
        get
        {
            switch (currentItem)
            {
                case Item.none:
                    return "none";
                case Item.woodensword:
                    animator.SetInteger("weapon", 1);
                    return "woodensword";
                case Item.ironsword:
                    animator.SetInteger("weapon", 2);
                    return "ironsword";
                    
                default:
                    return "none";
            }
        }
    }
}
