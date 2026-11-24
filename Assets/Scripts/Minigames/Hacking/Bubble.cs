using static UnityEngine.Mathf;
using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class Bubble : MonoBehaviour
{

    public int column;
    public int row;
    public Color color;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float destroySlerpAttribute;
    [SerializeField] private SpriteRenderer TileSpriteRenderer;
    public Matrix BubbleMatrix { get; private set; }

    public Bubble LeftNeighbour => GetNeighbour(-1, 0, () => column > 0);

    public Bubble RightNeighbour => GetNeighbour(1, 0, () => column < BubbleMatrix.width - 1);

    public Bubble UpNeighbour => GetNeighbour(0, 1, () => row < BubbleMatrix.height - 1);

    public Bubble DownNeighbour => GetNeighbour(0, -1, () => row > 0);

    public int points = 1;
    public TextMeshPro pointsTMP;

    private const float DESTROY_DELAY = 0.3f;
    private const float DESTROY_ITERATION = 200;
    private const float DESTROY_SLERP_CORRECTION = 0.02f;
    private Coroutine bubbleDestroyCoroutine;

    void Awake()
    {
        BubbleMatrix = FindAnyObjectByType<Matrix>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
        SpriteRenderer sr = this.gameObject.FindFirstComponentInChildWithTag<SpriteRenderer>("Symbol");
        sr.color = color;
    }

    private void OnMouseDown()
    {
        GameManagerHacking.Instance.controlMethod.OnMouseDownAction(this);
    }

    private void OnMouseUp()
    {
        GameManagerHacking.Instance.controlMethod.OnMouseUpAction();
    }

    public bool IsMoving(float moveTime)
    {
        return Abs(transform.position.x - column) > moveTime || Abs(transform.position.y - row) > moveTime;
    }

    public bool IsOutside(float margin)
    {
        return (transform.position.x + margin < 0 || transform.position.x >= BubbleMatrix.width - 1 + margin || transform.position.y + margin < 0 || transform.position.y >= BubbleMatrix.height - 1 + margin);
    }

    public bool IsNeighbourOfBubble(Bubble secondBubble)
    {
        return (Abs(row - secondBubble.row) + Abs(column - secondBubble.column) == 1);
    }

    /// <summary>
    /// Returns neighbour in given direction. 
    /// The direction is calculated from nextRow (y axis) and nextColumn (x axix).
    /// </summary>
    /// <param name="nextColumn"></param>
    /// <param name="nextRow"></param>
    /// <param name="edgePredicate">
    /// Function that checks if there is no edge in given direction
    /// </param>
    /// <returns></returns>
    private Bubble GetNeighbour(int nextColumn, int nextRow, Func<bool> edgePredicate)
    {
        if (edgePredicate.Invoke())
        {
            GameObject bubbleGameObject = GameManagerHacking.Instance.matrix.MatrixBubbles[column + nextColumn, row + nextRow];
            return bubbleGameObject == null ? null : bubbleGameObject.GetComponent<Bubble>();
        }
        return null;
    }

    /// <summary>
    /// Method which destroy bubble and shows points
    /// </summary>
    /// <param name="multiplier"></param>
    public void DestroyAndShowPoints(in int multiplier)
    {
        GameManagerHacking.Instance.matrix.ClearPosition(column, row);

        pointsTMP.text = (points * multiplier).ToString();
        pointsTMP.color = color;
        GameObject gm = Instantiate(pointsTMP.gameObject, this.transform.position, this.transform.rotation, null);
        Destroy(gm, DESTROY_DELAY);

        CheckDestroyCoroutine();
    }

    /// <summary>
    /// Checks if any another coroutine started before.
    /// </summary>
    private void CheckDestroyCoroutine()
    {
        if (bubbleDestroyCoroutine == null)
            bubbleDestroyCoroutine = StartCoroutine(ScaleMoveDestroy());
        else
            //if bug repeats
            Debug.LogError((string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), "Destoryed")));
    }

    /// <summary>
    /// Start scaling and moving bubble to target position. Next destrroy bubble.
    /// </summary>
    /// <returns></returns>
    IEnumerator ScaleMoveDestroy()
    {
        SetAppearanceToNotSignificant();

        for (int i = 0; i < DESTROY_ITERATION; i++)
        {
            this.transform.localScale -= new Vector3(1 / DESTROY_ITERATION, 1 / DESTROY_ITERATION, 1 / DESTROY_ITERATION);
            this.transform.position = Vector3.Slerp(this.transform.position, targetPosition, destroySlerpAttribute);
            destroySlerpAttribute += DESTROY_SLERP_CORRECTION * Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Set appearance to not significant
    /// </summary>
    private void SetAppearanceToNotSignificant()
    {
        Color transparentColor = new Color(0, 0, 0, 0);
        TileSpriteRenderer.color = transparentColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = transparentColor;
        sr = this.gameObject.FindFirstComponentInChildWithTag<SpriteRenderer>("Symbol");
        sr.color = Color.gray;
        sr.sortingOrder = -sr.sortingOrder;
    }
}
