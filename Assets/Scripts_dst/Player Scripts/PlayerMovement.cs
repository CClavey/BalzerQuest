using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private enum facingDirection {UP, DOWN, LEFT, RIGHT};
    private facingDirection facing = facingDirection.DOWN;
    public float dashRange;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    Vector2 targetPos;
    public ShopScript shopScript;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn manager in the new scene
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null && spawnManager.spawnPoint != null)
        {
            transform.position = spawnManager.spawnPoint.position;
            transform.rotation = spawnManager.spawnPoint.rotation; // Optional for setting rotation
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        //Finds the animator that this component is coded on (the player)
    }

    void Update()
    {
        TakeInput();
        if (!isDashing) Move();
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3(direction.x, direction.y).normalized;
        transform.Translate(moveDir * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (shopScript.ShopMenu.activeSelf) return;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            facing = facingDirection.UP;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            facing = facingDirection.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            facing = facingDirection.DOWN;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            facing = facingDirection.RIGHT;
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 currentPos = transform.position;
            targetPos = Vector2.zero;
            if(facing == facingDirection.UP)
            {
                targetPos.y = 1;
            }
            else if(facing == facingDirection.LEFT)
            {
                targetPos.x = -1;
            }
            else if (facing == facingDirection.RIGHT)
            {
                targetPos.x = 1;
            }
            else if (facing == facingDirection.DOWN)
            {
                targetPos.y = -1;
            }
            transform.Translate(targetPos * dashRange);
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
        print(animator.GetFloat("xDir"));
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("isDashing", true);
        Vector2 dashDirection = Vector2.zero;
        switch (facing)
        {
            case facingDirection.UP:
                dashDirection = Vector2.up;
                break;
            case facingDirection.DOWN:
                dashDirection = Vector2.down;
                break;
            case facingDirection.LEFT:
                dashDirection = Vector2.left;
                break;
            case facingDirection.RIGHT:
                dashDirection = Vector2.right;
                break;
        }
        transform.Translate(dashDirection * dashRange, Space.World);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        animator.SetBool("isDashing", false);
    }


}
