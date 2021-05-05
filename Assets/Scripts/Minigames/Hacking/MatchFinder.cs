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
    private static List<Bubble> MatchesForBubbleInDirection(Bubble bubble, Color searchedColor, Func<Bubble, Bubble> nextBubbleAccessor)
    {
        List<Bubble> matches = new List<Bubble>();
        for (Bubble currentBubble = nextBubbleAccessor(bubble); currentBubble && currentBubble.color.Equals(searchedColor); currentBubble = nextBubbleAccessor(currentBubble))
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
    public static List<Bubble> HorizontalMatchesForBubble(Bubble bubble)
    {
        Color searchedColor = bubble.color;
        List<Bubble> horizontalMatches = new List<Bubble>();
        horizontalMatches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.LeftNeighbour));
        horizontalMatches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.RightNeighbour));
        if (horizontalMatches.Count > 1) return horizontalMatches;
        return new List<Bubble>();
    }

    /// <summary>
    /// Returns list with all matched bubbles in vertical axis.
    /// </summary>
    /// <param name="bubble"></param>
    /// <returns></returns>
    public static List<Bubble> VerticalMatchesForBubble(Bubble bubble)
    {
        Color searchedColor = bubble.color;
        List<Bubble> verticalMaches = new List<Bubble>();
        verticalMaches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.UpNeighbour));
        verticalMaches.AddRange(MatchesForBubbleInDirection(bubble, searchedColor, (Bubble b) => b.DownNeighbour));
        if(verticalMaches.Count > 1) return verticalMaches;
        return new List<Bubble>();
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
    private static int RemoveMatchesIfExist(List<Bubble> matches, int multiplier)
    {
        int bubbleNumberToAdd = 1;
        if (matches.Count + bubbleNumberToAdd >= MINIMAL_NUMBER_OF_MATCHED_BUBBLES)
        {
            matches.ForEach(b =>
            {
                b.DestroyAndShowPoints(multiplier);
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
    public static int RemoveBubbleAndMatchesIfExist(Bubble bubble, ref int multiplier)
    {
        int numberOfHorizontalyMatched = RemoveMatchesIfExist(HorizontalMatchesForBubble(bubble),  multiplier);
        int numberOfVerticalyMatched = RemoveMatchesIfExist(VerticalMatchesForBubble(bubble), multiplier);
        if (numberOfHorizontalyMatched != 0 || numberOfVerticalyMatched != 0)
        {
            int numberOfAllMatched = numberOfHorizontalyMatched + numberOfVerticalyMatched + 1;
            GameManagerHacking.Instance.AddCurrentScore(numberOfAllMatched * bubble.points, multiplier);
            bubble.DestroyAndShowPoints(multiplier);
            multiplier++;
            return numberOfAllMatched;
        }
        return 0;
    }
}
