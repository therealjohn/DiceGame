using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideCheck : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerStay(Collider other)
    {
        var dice = other.GetComponentInParent<Dice>();

        if (dice == null)
            return;

        if (dice.IsDoneRolling && dice.sideValue < 0)
        {
            if (int.TryParse(other.gameObject.name, out int sideValue))
            {
                dice.sideValue = sideValue;
            }
            else
                Debug.LogWarning("The sides of dice should be named with numbers so it can be parsed correctly.");
        }
    }
}
