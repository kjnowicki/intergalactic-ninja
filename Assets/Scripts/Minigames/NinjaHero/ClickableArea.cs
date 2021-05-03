using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ClickableArea : Clickable
{
    public int elementLayerMask;
    private Collider2D clickableAreaCollider;
    public CancellationToken cancellationToken;
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
            clickableAreaCollider.OverlapCollider(contactFilter, colliders);
            Element element = colliders.First().gameObject.GetComponent<Element>();
            if(element) switch (touchType) {
                case Touch.Down:
                    element.Score(true);
                    break;
                case Touch.Up:
                case Touch.Drag:
                default:
                    element.Score(false);
                    break;
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
