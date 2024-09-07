using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameLibrary;


[CreateAssetMenu(fileName = "MotherCard", menuName = "SO/MotherCard", order = 0)]
public class CardSO : ScriptableObject
{
    public new string name;
    public Faction faction;
    public AttackType attackType;
    public List<Zone> zones;
    public Sprite image;
    public Sprite info;
    public List<Card> currentPos;
}