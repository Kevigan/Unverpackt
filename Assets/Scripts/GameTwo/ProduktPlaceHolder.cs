using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduktPlaceHolder : MonoBehaviour
{
    [SerializeField] private Produkt[] products;
    private List<string> names;
    HashSet<string> produktTypes = new HashSet<string>();

    private string actualProduct;
    private int number;

    void Start()
    {
        names = new List<string>();
        for (int i = 0; i < products.Length; i++)
        {
            names.Add(products[i].ProduktType.ToString());
        }
        foreach (string name in names)
        {
            produktTypes.Add(name);
        }
        SetSprite();

        
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void SetUiButtonsName()
    {
        number = Random.Range(0, 4);
    }

    public void SetSprite()
    {
        int i = Random.Range(0, products.Length);
        GetComponent<SpriteRenderer>().sprite = products[i].Sprite;
        actualProduct = products[i].ProduktType.ToString();
        produktTypes.Remove(products[i].ProduktType.ToString());
    }

    public void Button1(int i)
    {
        CheckAnswer(i);
    }
    public void Button2(int i)
    {
        CheckAnswer(i);
    }
    public void Button3(int i)
    {
        CheckAnswer(i);
    }

    private void CheckAnswer(int i)
    {

    }

}
