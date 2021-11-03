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

    
}
public enum ProduktType
{
    GemüseBrühe,
    MangoScheiben,
    MadrasCurry
}
