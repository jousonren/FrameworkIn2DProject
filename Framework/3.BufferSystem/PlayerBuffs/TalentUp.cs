using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentUp : MonoBehaviour {
    public GameObject Talent2;
    public GameObject All;
    public TrailRenderer[] TR;
    // Use this for initialization
    void Awake()
    {
        All.transform.position = GameControl.gameControl.Player.player.transform.position;
        for(int i = 0; i < 4; i++)
        {
            TR[i].sortingLayerName = "Player";
        }
    }
    public void Play2()
    {
        Destroy(All,0.7f);
        Talent2.SetActive(true);
        for (int i = 4; i < 8; i++)
        {
            TR[i].sortingLayerName = "Player";
        }
    }
    /*void FixedUpdate()
    {
        All.transform.position = GameControl.gameControl.Player.player.transform.position;
    }*/
}
