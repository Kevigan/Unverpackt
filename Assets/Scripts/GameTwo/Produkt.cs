using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Produkt : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite { get => sprite; set { sprite = value; } }
    [SerializeField] private ProduktType produktType;
    public ProduktType ProduktType { get => produktType; set { produktType = value; } }

    public string answer1;
    public string answer2;
    public string answer3;

}
public enum ProduktType
{
    GemüseBrühe,
    MangoScheiben,
    MadrasCurry,
    Berglinsen,
    CafeCreme,
    Cashewkerne,
    Chiasamen,
    Birdseyechili,
    Couscous,
    Datteln,
    Erdnuesse,
    Espresso,
    Gojibeeren,
    Haselnusskerne,
    Jasminreis,
    KaffeeSuave,
    Kichererbsen,
    Kidneybohnen, 
    Kokosbluetenzucker,
    KraueterDeProvince,
    Kuerbiskerne,
    Kurkuma,
    Lorbeerblaetter,
    Mandeln,
    PaprikaEdelsuess,
    Paranuesse,
    PfefferSchwarz,
    Quinoa,
    RoteLinsen,
    SesamUngeschaelt,
    SesameGeschaelt,
    Sonnenblumenkerne,
    Walnusshaelften
}
