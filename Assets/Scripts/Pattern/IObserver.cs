using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public interface IObserver
{
    public void OnNotify(System.Enum action, Card card = null);
}
