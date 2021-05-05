using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    protected Rigidbody2D elementRigidbody;
    public Spawner spawner;

    public ElementState elementState;
    public float Height { get; set; }
    public bool scored = false;

    private const string SPAWNER_TAG = "Spawner";
    private const string CLIKABLE_AREA_TAG = "ClikableArea";
    private const string ACTION_STOP_TAG = "ActionStop";

    public virtual void Awake()
    {
        elementRigidbody = GetComponent<Rigidbody2D>();
        elementState = ElementState.NotSpawned;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Height = spriteRenderer.bounds.max.y - spriteRenderer.bounds.min.y;
    }
    /// <summary>
    /// Unique spawn behavior
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="spawner"></param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public abstract Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken);

    /// <summary>
    /// Destroy object. In future can be useful for animation or on death method. 
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// </summary>
    public virtual void DestroyElement(CancellationToken cancellationToken)
    {
        Destroy(gameObject);
    }

    public virtual void Score(bool startedScoring)
    {
        if (!scored && startedScoring) CorrectHit();
    }

    /// <summary>
    /// Change state despite of trigger
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case SPAWNER_TAG:
                elementState = ElementState.Spawned;
                break;
            case CLIKABLE_AREA_TAG:
                if (elementState != ElementState.CorrectHit)
                {
                    WrongHit();
                }
                break;
            case ACTION_STOP_TAG:
                elementState = ElementState.ActionStop;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Behavior when element is clicked properly.
    /// </summary>
    protected virtual void CorrectHit()
    {
        scored = true;
        elementState = ElementState.CorrectHit;
        GetComponent<SpriteRenderer>().color = Color.green;
        NinjaHeroGameManager.Instance.IncrementScore();
    }
    /// <summary>
    /// Method used when element is missed. 
    /// </summary>
    protected virtual void WrongHit()
    {
        elementState = ElementState.WrongHit;
        GetComponent<SpriteRenderer>().color = Color.red;
        NinjaHeroGameManager.Instance.GameOver();
    }
    public enum ElementState
    {
        /// <summary>
        /// Starting state
        /// </summary>
        NotSpawned,
        /// <summary>
        /// Stete between spawner and clikable area
        /// </summary>
        Spawned,
        /// <summary>
        /// After correct hit element
        /// </summary>
        CorrectHit,
        /// <summary>
        /// After missed element
        /// </summary>
        WrongHit,
        /// <summary>
        /// Special for helisa
        /// </summary>
        ActionStop
    }
}
