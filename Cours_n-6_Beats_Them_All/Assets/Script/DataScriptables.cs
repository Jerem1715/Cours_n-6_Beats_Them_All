using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Test/Health", order = 1)]
public class DataScriptable : ScriptableObject
{
    public int startHealth;
    public int doDamage;

}
