using UnityEngine;
using System.Collections.Generic;
public class WorldState : MonoBehaviour
{
    public HashSet<string> defeatedEnemies = new();

    void Awake() => ServiceLocator.Register(this);
}
