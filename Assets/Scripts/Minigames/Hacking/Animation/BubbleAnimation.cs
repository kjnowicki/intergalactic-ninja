using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Random;

public class BubbleAnimation : MonoBehaviour
{
    private const float IDLE_ANIMATIONTIME= 4;

    private Animator bubbleAnimator;
    private Animator symbolAnimator;

    private void Awake()
    {
        bubbleAnimator = GetComponent<Animator>();
        symbolAnimator = this.gameObject.FindFirstComponentInChildWithTag<Animator>("Symbol");
    }

    /// <summary>
    /// Plays idel animation on animator
    /// </summary>
    public void IdleAnimation() 
    {
        if (!bubbleAnimator.enabled)
            bubbleAnimator.enabled = true;
        else
        bubbleAnimator.SetTrigger("Idle");
    }

    public static async void RandomIdleAnimation(Matrix matrix)
    {
        while (matrix != null)
        {
            List<BubbleAnimation> objectToAnimate = new List<BubbleAnimation>();
            List<int> usedRows = new List<int>();
            List<int> usedColumns = new List<int>();

            for (int i = 0; i < Min(matrix.width, matrix.height); i++)
            {
                IEnumerable<int> range = Enumerable.Range(0, matrix.width).Except(usedColumns);
                int randomColumnIndex = Range(0, range.Count() - 1);
                usedColumns.Add(range.ElementAt(randomColumnIndex));

                range = Enumerable.Range(0, matrix.height).Except(usedRows);
                int randomRowIndex = Range(0, range.Count() - 1);
                usedRows.Add(range.ElementAt(randomRowIndex));

                GameObject bubble = matrix.MatrixBubbles[usedColumns.Last(), usedRows.Last()];

                if (bubble != null)
                    objectToAnimate.Add(bubble.GetComponent<BubbleAnimation>());
            }
            foreach (BubbleAnimation bubbleAnimation in objectToAnimate) { bubbleAnimation.IdleAnimation(); }

            await Task.Delay(TimeSpan.FromSeconds(IDLE_ANIMATIONTIME));
        }


    }

}
