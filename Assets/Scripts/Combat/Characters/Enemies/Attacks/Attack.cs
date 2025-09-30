
using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public abstract IEnumerator ActivateAttack(float levelModifier);
}