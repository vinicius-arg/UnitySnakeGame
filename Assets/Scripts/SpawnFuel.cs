using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFuel : MonoBehaviour
{
    public GameObject FuelPrefab;
    //Borders
    public Transform BorderTop;
    public Transform BorderLeft;
    public Transform BorderRight;
    public Transform BorderBottom;

    public void Spawn()
    {
        int x = (int)Random.Range(BorderLeft.position.x + 10, BorderRight.position.x - 10);
        int y = (int)Random.Range(BorderTop.position.y + 10, BorderBottom.position.y - 10);

        Instantiate(FuelPrefab, new Vector2(x, y), Quaternion.identity);
    }

    void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 7f);
    }
}
