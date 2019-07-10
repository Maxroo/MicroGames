using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugGameManager : MonoBehaviour
{
    public static PlugGameManager instance;
    private int plugToSpawn;
    public GameObject[] plugs;
    public GameObject startPoint;

    private void Awake() {
        plugToSpawn = Random.Range(0, 3);
        Instantiate(plugs[plugToSpawn], startPoint.transform.position, startPoint.transform.rotation);
    }
}
