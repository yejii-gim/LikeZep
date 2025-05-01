using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rd;

    [SerializeField] private SpriteRenderer characterRenderer; // 캐릭터 렌더러
    [SerializeField] private int moveSpeed;
    public SpriteRenderer CharacterRenderer { get { return characterRenderer; } }
    protected Vector2 movementDirection = Vector2.zero; 
    public Vector2 MovementDirection { get { return movementDirection; } }
    protected Vector2 lookDirection = Vector2.zero; 
    public Vector2 LookDirection { get { return lookDirection; } }

    AnimationHandler animationHandler;
    
    protected virtual void Awake()
    {
        characterRenderer = GetComponentInChildren<SpriteRenderer>();
        _rd = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {
    }

    private void Movement(Vector2 direction)
    {
        direction = direction * moveSpeed;
        _rd.velocity = direction;
        animationHandler.Move(direction);
 
    }

    protected void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }
    public virtual void Death()
    {
        _rd.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        Destroy(gameObject, 1f);
    }
}