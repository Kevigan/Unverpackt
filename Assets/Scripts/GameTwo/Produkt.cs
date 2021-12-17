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
    GemueseBruehe,
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
    Sesam_Ungeschaelt,
    Sesame_Geschaelt,
    Sonnenblumenkerne,
    Walnusshaelften,
    Akazienhonig,
    Belugalinsen,
    HonigFruehjahrsbluete,
    Kuemmel,
    NaturreisMittelKorn,
    Parboiledreis,
    Rauchsalz,
    Sauerkirschen, 
    Schwarzkuemmel,
    SeitanHack,
    SeitanTamaris,
    SommerblueteHonig,
    Trinkschokolade, 
    Zimt
}
