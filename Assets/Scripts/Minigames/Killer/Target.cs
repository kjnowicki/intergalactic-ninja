using UnityEngine;

public class Target : MonoBehaviour
{
    public Animator animator;

    public float LifeTime { get; set; }

    private float lifeCounter;

    private void Start()
    {

        lifeCounter = 0;
        animator.speed = 5 / LifeTime;

    }

    private void Update()
    {
        lifeCounter += Time.deltaTime;
        CheckIfTargetWasTouched();
        if (LifeTime <= lifeCounter)
        {
            Destroy(gameObject);
            GameManagerKiller.Instance.GameOver();
        }
    }

    /// <summary>
    /// Checks if any touch hit a target
    /// </summary>
    private void CheckIfTargetWasTouched()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (gameObject.GetComponent<CircleCollider2D>().OverlapPoint(touchPosition))
            {
                Destroy(gameObject);
                GameManagerKiller.Instance.CurrentScore++;
            }
        }
    }
}

