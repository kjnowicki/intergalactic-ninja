using System;
using System.Collections.Generic;
using UnityEngine;

public static class MatchFinder
{
    private const int MINIMAL_NUMBER_OF_MATCHED_BUBBLES = 3;

    /// <summary>
    /// Returns all matched bubbles in direction given by nextBubbleAccessor parameter until edge or bubble with different color apeeared.
    /// </summary>
    /// <param name="bubble"></param>
    /// <param name="searchedColor"></param>
    /// <param name="nextBubbleAccessor">
    /// Function that retuns given neighbour of the bubble
    /// </param>
    /// <returns></returns>
    private static List<Bubble> MatchesForBubbleInDirection(Bubble bubble, BubbleColor searchedColor, Func<Bubble, Bubble> nextBubbleAccessor)
    {
        List<Bubble> matches = new List<Bubble>();
        for (Bubble currentBubble = nextBubbleAccessor(bubble); currentBubble && currentBubble.color == searchedColor; currentBubble = nextBubbleAccessor(currentBubble))
        {
            matches.Add(currentBubble);
        }
        return matches;
    }

    /// <summary>
    /// Returns list with all matched bubbles in horizontal axis.
    /// </summary>
    /// <param name="bubble"></param>
    /// <returns></returns>
    private static List<Bubble> HorizontalMatchesForBubble(Bubble bubble)
    {
        BubbleColor searchedColor = bubble.color;
        List<Bubble> horizontalMatches = new List<Bubble>();
        horizontalMatches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.LeftNeighbour));
        horizontalMatches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.RightNeighbour));
        return horizontalMatches;
    }

    /// <summary>
    /// Returns list with all matched bubbles in vertical axis.
    /// </summary>
    /// <param name="bubble"></param>
    /// <returns></returns>
    private static List<Bubble> VerticalMatchesForBubble(Bubble bubble)
    {
        BubbleColor searchedColor = bubble.color;
        List<Bubble> verticalMaches = new List<Bubble>();
        verticalMaches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.UpNeighbour));
        verticalMaches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.DownNeighbour));
        return verticalMaches;
    }

    /// <summary>
    /// Removes bubbles if number of matches is equal or greater than threshold.
    /// </summary>
    /// <param name="matches">
    /// List with matched bubbles.
    /// </param>
    /// <returns>
    /// true if there are at least MINIMAL_NUMBER_OF_MATCHED_BUBBLES in row, false otherwise
    /// </returns>
    private static int RemoveMatchesIfExist(List<Bubble> matches)
    {
        int bubbleNumberToAdd = 1;
        if (matches.Count + bubbleNumberToAdd >= MINIMAL_NUMBER_OF_MATCHED_BUBBLES)
        {
            matches.ForEach(b =>
            {
                GameManagerHacking.Instance.matrix.AddEmptyPositionToBuffer(b.column, b.row);
                GameObject.Destroy(b.gameObject);
            });
            return matches.Count;
        }
        return 0;
    }

    /// <summary>
    /// Checks all possible matches for a bubble. If there are, the bubble is destoyed.
    /// </summary>
    /// <param name="bubble"></param>
    /// <returns>
    /// true if there was a match, false otherwise
    /// </returns>
    public static int RemoveBubbleAndMatchesIfExist(Bubble bubble)
    {
        int numberOfHorizontalyMatched = RemoveMatchesIfExist(HorizontalMatchesForBubble(bubble));
        int numberOfVerticalyMatched = RemoveMatchesIfExist(VerticalMatchesForBubble(bubble));
        if (numberOfHorizontalyMatched != 0 || numberOfVerticalyMatched != 0)
        {
            GameManagerHacking.Instance.matrix.AddEmptyPositionToBuffer(bubble.column, bubble.row);
            GameObject.Destroy(bubble.gameObject);
            return numberOfHorizontalyMatched + numberOfVerticalyMatched + 1;
        }
        return 0;
    }
}
