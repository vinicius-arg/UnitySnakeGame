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
    // Start is called before the first frame update
    void Spawn()
    {
        int x = (int)Random.Range(BorderLeft.position.x, BorderRight.position.x);
        int y = (int)Random.Range(BorderTop.position.y, BorderBottom.position.y);

        Instantiate(FuelPrefab, new Vector2(x, y), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spawn();
    }
    void Start()
    {
        Spawn();
    }
}
