using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPools", menuName = "ScriptableObjects/EnemyPools", order = 1)]
public class EnemyPoolsScriptableObject : ScriptableObject
{
    public string PebblesPool;
    public string SkullsPool;
    public string BatsPool;
    public string RatsPool;
    public string SlimesPool;
    public string GolemsPool;
}
