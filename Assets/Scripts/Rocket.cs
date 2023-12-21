using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Rocket : MonoBehaviour
{
    public Sprite Up, Down, Left, Right;
    public SpriteRenderer SpriteRenderer;
    public List<Transform> Tail = new List<Transform>();
    public List<Vector3> TailPositions;
    public GameObject tailPrefab;
    public int TailSize = 5;
    public bool caught = false;
    int SaveCoordsTimer = 0;
    float Speed = 0.05f;
  
    // Direção do movimento
    Vector2 direction = Vector2.right;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Move", 0.05f, Speed);
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && direction != Vector2.down)
        {
            direction = Vector2.up;
            SpriteRenderer.sprite = Up;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && direction != Vector2.right)
        {
            direction = Vector2.left;
            SpriteRenderer.sprite = Left;
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && direction != Vector2.up)
        {
            direction = Vector2.down;
            SpriteRenderer.sprite = Down;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && direction != Vector2.left)
        {
            direction = Vector2.right;
            SpriteRenderer.sprite = Right;
        }
    }

    private Vector3 CalculateGap()
    {
        int GapSize = 0;

        Vector2 Gap = Vector2.zero;
        if (direction == Vector2.up)
            Gap = new Vector2(0, -GapSize);
        else if (direction == Vector2.down)
            Gap = new Vector2(0, GapSize);
        else if (direction == Vector2.right)
            Gap = new Vector2(-GapSize, 0);
        else if (direction == Vector2.left)
            Gap = new Vector2(GapSize, 0);
        return Gap;
    }

    private void SaveCoords()
    {
        TailPositions.Insert(0, transform.position);

        // Guarda 2 posições a mais que o tamanho da cauda
        if (TailPositions.Count > TailSize + 2)
        {
            TailPositions.Remove(TailPositions.Last());
        }
    }

    public void Move()
    {
        SaveCoordsTimer++;

        // Salva posições do foguete a cada 3 iterações de Move
        if (SaveCoordsTimer == 3)
        {
            SaveCoords();
            SaveCoordsTimer = 0;
        }

        // Reposiciona elementos da cauda
        for (int i = 0; i < Tail.Count; i++)
        {
            Tail[i].position = TailPositions[i+1];
        }

        // Muda a direção do foguete
        transform.Translate(direction);

        if (caught)
        {
            GameObject instantiatedTail = (GameObject)Instantiate(tailPrefab, TailPositions.Last(), Quaternion.identity);
            // Remove colisões para os 2 primeiros elementos da cauda
            if (Tail.Count < 5)
            {
                Collider2D TailBegin = instantiatedTail.GetComponent<Collider2D>();
                TailBegin.enabled = false;
            }

            Tail.Add(instantiatedTail.transform);
            caught = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("FuelPrefab"))
        {
            TailSize++;
            caught = true;
            Destroy(collision.gameObject);
        }
        else
        {
            print("Game over");
        }
    }
}