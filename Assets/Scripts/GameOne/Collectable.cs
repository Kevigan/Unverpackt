using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;
    [SerializeField] private Color color;
    [SerializeField] private int points = 50;
    [SerializeField] private GameObject floatingTextPrefab;
    private string floatText;
    private void Start()
    {
        floatText = fruitType.ToString();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            GameManager.Main.FruitAdder(fruitType);
            GameManager.Main.UpdateScoreFruits(points);
            SoundManager.Main.ChooseSound(SoundType.PlayerCollect);
            GameObject floatText = Instantiate(floatingTextPrefab, player.transform.position, Quaternion.identity);
            floatText.GetComponentInChildren<TextMeshPro>().text = fruitType.ToString();
            floatText.GetComponentInChildren<TextMeshPro>().outlineColor = color;
            floatText.transform.position = new Vector3(player.transform.position.x, floatText.transform.position.y);
            Destroy(floatText, 1);
            
            Destroy(gameObject);
        }
    }
}

public enum FruitType
{
    Kaffee,
    Hirse,
    Linsen,
    Goji,
    Reis,
    Pineapple
}
