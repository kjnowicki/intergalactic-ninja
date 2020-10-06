using UnityEngine;

public class MovableObjectsContainer : MonoBehaviour
{
    RectTransform movableObjectsContainerRectTransform;

    /// <summary>
    /// Initializes MovableObjectsContainer.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    public void Initialize(RectTransform gameAreaRectTransform)
    {
        transform.position = gameAreaRectTransform.position;
        movableObjectsContainerRectTransform = GetComponent<RectTransform>();
        movableObjectsContainerRectTransform.sizeDelta = gameAreaRectTransform.sizeDelta;
        movableObjectsContainerRectTransform.position = gameAreaRectTransform.position;
    }
}
