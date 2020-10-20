using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension 
{
    /// <summary>
    /// Method works similarly to FindComponentInChild(), but finds only components with specific Tag
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <param name="forceActive">Should Component on inactive GameObjects be included in the found set? includeInactive decides which children of the GameObject will be searched. The GameObject that you call GetComponentsInChild is always searched regardless.</param>
    /// <returns>It returns Component with specific Tag</returns>
    public static T FindFirstComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        T[] list = parent.GetComponentsInChildren<T>(forceActive);
        foreach (T t in list)
        {
            if (t.CompareTag(tag) == true)
            {
                return t;
            }
        }
        return null;
    }

    /// <summary>
    /// Method works similarly to FindComponentInChildren(), but finds only components with specific Tag
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <param name="forceActive">Should Components on inactive GameObjects be included in the found set? includeInactive decides which children of the GameObject will be searched. The GameObject that you call GetComponentsInChildren on is always searched regardless.</param>
    /// <returns></returns>
    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }
}
