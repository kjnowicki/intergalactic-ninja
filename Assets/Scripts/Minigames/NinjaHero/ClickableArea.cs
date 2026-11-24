using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ClickableArea : Clickable
{
    private Collider2D clickableAreaCollider;
    private void Start()
    {
        clickableAreaCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Score on first element in area
    /// </summary>
    /// <param name="touchType">For scoring condition</param>
    private void OnTouchedClickableArea(Touch touchType)
    {
        if (clickableAreaCollider.IsTouchingLayers())
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = Physics2D.AllLayers,
                useTriggers = true
            };
            clickableAreaCollider.Overlap(contactFilter, colliders);
            colliders.RemoveAll(collider => collider.tag.Contains(clickableAreaCollider.tag));
            if(colliders.Count > 0)
            {
                var elements = colliders
                    .OrderBy(collider => collider.transform.position.y)
                    .Select(collider => collider.GetComponent<Element>())
                    .Where(colliderElement => !colliderElement.scored);
                if (elements.Count() > 0)
                {
                    var element = elements.FirstOrDefault();
                    if(element) element.Score(touchType == Touch.Down);
                }
            }
        }
    }

    public override void OnTouchDown()
    {
        OnTouchedClickableArea(Touch.Down);
    }

    public override void OnTouchDrag()
    {
        OnTouchedClickableArea(Touch.Drag);
    }

    public override void OnTouchUp()
    {
        OnTouchedClickableArea(Touch.Up);
    }

    public enum Touch
    {
        Drag,
        Up,
        Down
    }
}
