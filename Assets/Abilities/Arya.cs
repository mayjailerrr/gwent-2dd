using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arya : MonoBehaviour
{
   public void Attack()
    {
        Strip[] allStrips = GameObject.FindObjectsOfType<Strip>();

        foreach (Strip strip in allStrips)
        {
            strip.Arya();
        }
    }
}
