using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRubberBand : MonoBehaviour
{
    private string PLAYER_TAG = "Player";

    private GameObject movableObjectsColection;

    [SerializeField] private BoxCollider2D restrictedArea;

    [Range(0.0f, 15.0f)]
    [SerializeField] private float rubberBandStrength = 5f;

    [Range (0.0f, 10.0f)]
    [SerializeField] private float downwardForce = 1f;

    [Header ("Restricted area treshold")]
    [Range (0.5f, 1f)]
    [SerializeField] private float restrictedAreaHeight = 0.85f;

    private void Awake()
    {
        movableObjectsColection = FindObjectOfType<MovableObjectsContainer>().gameObject;
    }

    private void Update()
    {
        CameraMovement();
    }

    public void Initialize(Vector2 restrictedAreaPosition,Vector2 restrictedAreaSize)
    {
        restrictedArea.transform.position = new Vector2(restrictedAreaPosition.x + (restrictedAreaSize.x / 2), -restrictedAreaPosition.y * restrictedAreaHeight);
        restrictedArea.size = new Vector2(restrictedAreaSize.x * 2f, restrictedAreaSize.y);
    }
    /// <summary>
    /// Moves all movable objects to create ilusion of camera movement
    /// </summary>
    private void CameraMovement()
    {
        float deltaTime = Time.deltaTime;
        float moveValue = downwardForce * deltaTime;
        foreach (Transform movablePosition in movableObjectsColection.GetComponentsInChildren<Transform>())
        {
            if (movablePosition.parent == movableObjectsColection.transform)
                movablePosition.position += Vector3.down * moveValue;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == PLAYER_TAG)
        {
            RubberBand();
        }
    }
    /// <summary>
    /// Rubber band mechanizm to restrict player from jumping to high
    /// </summary>
    private void RubberBand()
    {
        foreach (Transform movablePosition in movableObjectsColection.GetComponentsInChildren<Transform>())
        {
            Vector2 position = movablePosition.position;
            if (movablePosition.parent == movableObjectsColection.transform)
                movablePosition.position = Vector2.Lerp(position, new Vector2(position.x, position.y - downwardForce), rubberBandStrength * Time.deltaTime);
        }
    }
}
