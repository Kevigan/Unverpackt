using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduktPlaceHolder : MonoBehaviour
{
    [SerializeField] private List<Produkt> products = new List<Produkt>();
    private List<Produkt> _products = new List<Produkt>();

    private Produkt actualProduct;
    private int number;

    void Start()
    {
        GameManager.Main.gameStateGame2 = GameStateGame2.playing;
        UIManager.SetNextProduct += PickRandmProduct;
        UIManager.SetNextProduct += SetButtonNames;

        _products = products;

        PickRandmProduct();

        SetButtonNames();

    }

    private void PickRandmProduct()
    {
        if (products.Count >= 1)
        {
            UIManager.Main.SetBlurry(true);
            int i = Random.Range(0, products.Count);
            actualProduct = products[i];
            GetComponent<SpriteRenderer>().sprite = products[i].Sprite;
            GameManager.Main.actualProductName = actualProduct.ProduktType.ToString();
            _products.Remove(actualProduct);
        }
        else
        {
            UIManager.Main.ChangeUIStateGame2(UIStateGame2.NoProductLeftPanel);
        }
    }

    private void SetButtonNames()
    {
        List<string> tempList = new List<string>();
        tempList.Add(actualProduct.answer1);
        tempList.Add(actualProduct.answer2);
        tempList.Add(actualProduct.answer3);

        int i = Random.Range(0, tempList.Count);
        UIManager.Main.nameButton1.text = tempList[i];
        tempList.RemoveAt(i);

        int j = Random.Range(0, tempList.Count);
        UIManager.Main.nameButton2.text = tempList[j];
        tempList.RemoveAt(j);

        UIManager.Main.nameButton3.text = tempList[0];
    }
}
