using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameLibrary;


[CreateAssetMenu(fileName = "UnityCardSO", menuName = "SO/UnityCardSO")]

public class UnityCardSO : CardSO
{
    [SerializeField] double initialPower = 0;

    public Rank rank;

    public double InitialPower { get => initialPower; }
}