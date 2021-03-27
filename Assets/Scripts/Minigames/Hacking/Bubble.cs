using static UnityEngine.Mathf;
using UnityEngine;
using System;
using TMPro;

public class Bubble : MonoBehaviour
{

    public int column;
    public int row;
    public Color color;
    public Matrix BubbleMatrix { get; private set; }

    public Bubble LeftNeighbour => GetNeighbour(-1, 0, () => column > 0);

    public Bubble RightNeighbour => GetNeighbour(1, 0, () => column < BubbleMatrix.width - 1);

    public Bubble UpNeighbour => GetNeighbour(0, 1, () => row < BubbleMatrix.height - 1);

    public Bubble DownNeighbour => GetNeighbour(0, -1, () => row > 0);

    public int points = 1;
    public TextMeshPro pointsTMP;
    private const float DESTROY_DELAY = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        BubbleMatrix = FindObjectOfType<Matrix>();
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
            return BubbleMatrix.MatrixBubbles[column + nextColumn, row + nextRow].GetComponent<Bubble>();
        return null;
    }

    /// <summary>
    /// Method which shows points
    /// </summary>
    /// <param name="multiplier"></param>
    public void ShowPoints(in int multiplier)
    {
        pointsTMP.text = (points * multiplier).ToString();
        pointsTMP.color = color;
        GameObject gm = Instantiate(pointsTMP.gameObject, this.transform.position, this.transform.rotation, null);
        GameObject.Destroy(gm, DESTROY_DELAY);
    }

}
