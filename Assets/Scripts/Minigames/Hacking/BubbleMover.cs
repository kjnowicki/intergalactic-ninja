using UnityEngine;
using System;
using System.Threading.Tasks;

public static class BubbleMover
{
    private const float MOVE_STEP = 0.1f;

    private const double MOVE_FRAME_TIME = 0.02;

    private const float FADING_MARGIN = MOVE_STEP * 2;

    /// <summary>
    /// Changes bubble's position on screen and in MatrixBubbles list.
    /// </summary>
    /// <param name="bubble"></param>
    /// <param name="targetPosition"></param>
    public static async Task ChangePosition(Bubble bubble, Vector2 targetPosition)
    {
        await ChangePosition(bubble, targetPosition, MOVE_FRAME_TIME);
    }

    /// <summary>
    /// Changes bubble's position on screen and in MatrixBubbles list.
    /// </summary>
    /// <param name="bubble"></param>
    /// <param name="targetPosition"></param>
    /// <param name="moveFrameTime"></param>
    public static async Task ChangePosition(Bubble bubble, Vector2 targetPosition, double moveFrameTime)
    {
        while (bubble.IsMoving(MOVE_STEP))
        {
            bubble.transform.position = Vector2.MoveTowards(bubble.transform.position, targetPosition, MOVE_STEP);
            await Task.Delay(TimeSpan.FromSeconds(moveFrameTime));
            if (!bubble.IsOutside(FADING_MARGIN)) bubble.gameObject.SetActive(true);
        }
        bubble.transform.position = targetPosition;
    }

    /// <summary>
    /// Moves bubble to disired position
    /// </summary>
    /// <param name="bubble"></param>
    /// <param name="targetPosition"></param>
    /// <param name="moveTimeMultiplier"></param>
    /// <returns></returns>
    public static async Task MoveBubble(Bubble bubble, Vector2 targetPosition, int moveTimeMultiplier)
    {
        bubble.column = (int)targetPosition.x;
        bubble.row = (int)targetPosition.y;
        await Task.WhenAll(ChangePosition(bubble, targetPosition, 0.03 / moveTimeMultiplier));
    }

    /// <summary>
    /// Swaps two bubbles positions on screen and replaces their position in MatrixBubbles list.
    /// </summary>
    /// <param name="bubble1"></param>
    /// <param name="bubble2"></param>
    public static async Task SwapBubbles(Bubble bubble1, Bubble bubble2)
    {
        SwapCoords(bubble1, bubble2);
        Vector2 firstBubbleCellPosition = new Vector2(bubble1.column, bubble1.row);
        Vector2 secondBubbleCellPosition = new Vector2(bubble2.column, bubble2.row);
        await Task.WhenAll(ChangePosition(bubble1, firstBubbleCellPosition), ChangePosition(bubble2, secondBubbleCellPosition));
    }

    /// <summary>
    /// Swaps bubbles coords.
    /// </summary>
    /// <param name="bubble1"></param>
    /// <param name="bubble2"></param>
    private static void SwapCoords(Bubble bubble1, Bubble bubble2)
    {
        int row = bubble1.row;
        int column = bubble1.column;

        bubble1.row = bubble2.row;
        bubble1.column = bubble2.column;

        bubble2.row = row;
        bubble2.column = column;
    }
}
