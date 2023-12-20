using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rocket : MonoBehaviour
{
    public Sprite Up, Down, Left, Right;
    private SpriteRenderer SpriteRenderer;
    List<Transform> Tail = new List<Transform>();
    // Direção do movimento
    Vector2 direction = Vector2.right;
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        float Speed = 0.05f;
        InvokeRepeating("Move", 0.3f, Speed);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Impede a mudança pro sentido contrário ao movimento anterior
            // (choque com a cauda)
            if (direction != Vector2.down)
            {
                direction = Vector2.up;
                SpriteRenderer.sprite = Up;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (direction != Vector2.right)
            {
                direction = Vector2.left;
                SpriteRenderer.sprite = Left;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (direction != Vector2.up) 
            { 
                direction = Vector2.down;
                SpriteRenderer.sprite = Down;            
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (direction != Vector2.left)
            {
                direction = Vector2.right;
                SpriteRenderer.sprite = Right;
            }
        }
    }

    void Move()
    {
        transform.Translate(direction);
    }

}
