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
        movableObjectsColection = FindAnyObjectByType<MovableObjectsContainer>().gameObject;
    }

    private void Update()
    {
        CameraMovement();
    }

    public void Initialize(Vector2 restrictedAreaPosition, Vector2 restrictedAreaSize)
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
        Vector3 positionChange = Vector3.down * moveValue;
        WallJumpingGameManager.Instance.AddScore(moveValue);

        foreach (Transform movablePosition in movableObjectsColection.GetComponentsInChildren<Transform>())
        {
            if (movablePosition.parent == movableObjectsColection.transform)
            {
                movablePosition.position += positionChange;
            }
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
    /// Rubber band mechanism to restrict player from jumping to high
    /// </summary>
    private void RubberBand()
    {
        float bandStrength = downwardForce * rubberBandStrength;
        WallJumpingGameManager.Instance.AddScore(bandStrength * Time.deltaTime);
        foreach (Transform movablePosition in movableObjectsColection.GetComponentsInChildren<Transform>())
        {
            Vector2 position = movablePosition.position;
            if (movablePosition.parent == movableObjectsColection.transform)
            {
                movablePosition.position = Vector2.Lerp(position, new Vector2(position.x, position.y - bandStrength), Time.deltaTime);
            }
        }
    }
}