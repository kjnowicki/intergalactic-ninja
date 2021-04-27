using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ClickableArea : Clickable
{
    public int elementLayerMask;
    private Collider2D clickableAreaCollider;
    private bool tap;
    public CancellationToken cancellationToken;
    private void Start()
    {
        clickableAreaCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// On click event instructions for patern tile
    /// </summary>
    private async void TouchedClickableArea()
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
            foreach (Collider2D coll in colliders)
            {
                await coll.gameObject.GetComponent<Element>().HitElement(tap, cancellationToken);
            }
        }
    }

    override public void OnTouchDown()
    {
        tap = true;
    }

    public override void OnTouchDrag()
    {
        TouchedClickableArea();
        tap = false;
    }
}
