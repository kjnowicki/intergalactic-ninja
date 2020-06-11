using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SpriteRenderer image;

    [Range(0.0f, 1.0f)]
    public float fadeingPercentagePerStep;

    public float LifeTime { get; set; }

    private void Start()
    {
        StartCoroutine(Fade());
    }

    private void Update()
    {
        CheckIfTargetWasTouched();
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
                GameManagerKiller.Instance.AddScore();
            }
        }
    }
    /// <summary>
    /// Fades alpha of targets image
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade()
    {
        float fadeStepTime = fadeingPercentagePerStep * LifeTime;
        int fadeingStepsAmmount = (int)(1 / fadeingPercentagePerStep);
        for (int i = 0; i < fadeingStepsAmmount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Max(image.color.a - fadeingPercentagePerStep, 0f));
            yield return new WaitForSeconds(fadeStepTime);
        }

        GameManagerKiller.Instance.GameOver();
        Destroy(gameObject);
        yield return null;
    }
}

