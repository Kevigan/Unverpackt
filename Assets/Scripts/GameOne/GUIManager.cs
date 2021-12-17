using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Main;

    [SerializeField] private GameObject floatingScoreText;
    [HideInInspector]
    public int score = 0;
    public string scoreText = "";

    private void Awake()
    {
        if(Main == null)
        {
            Main = this;
        }else if(Main != this)
        {
            Destroy(this);
        }
    }

    public void PlayFloatigTextCoins(Vector3 pos)
    {
        Instantiate(floatingScoreText, pos + new Vector3(0, 1, -.25f), Quaternion.identity);
    }
}
