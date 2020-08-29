using UnityEngine;

public class GameArea : MonoBehaviour
{
    private RectTransform gameAreaRectTransform;
    private WallGenerator wallGenerator;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="heightRatio"></param>
    public void Initialize(float heightRatio, int wallHeightAdditional)
    {
        InitializePosition(heightRatio);
        InitializePlayer();
        InitializeWallGenerator(wallHeightAdditional);
    }

    /// <summary>
    /// Initializes GameArea position.
    /// </summary>
    /// <param name="heightRatio"></param>
    private void InitializePosition(float heightRatio)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 gameAreaBounds = new Vector2(screenBounds.x, screenBounds.y * heightRatio);
        gameAreaRectTransform.sizeDelta = gameAreaBounds * 2;
        gameAreaRectTransform.anchoredPosition = new Vector2(-gameAreaBounds.x, -screenBounds.y);
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    private void InitializePlayer()
    {
        float playerInitialYCoord = 1;
        Player player = GetComponentInChildren<Player>();
        Vector2 playerInitialPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, playerInitialYCoord);
        player.Initialize(playerInitialPosition);
    }

    private void InitializeWallGenerator(int wallHeightAdditional)
    {
        wallGenerator = GetComponent<WallGenerator>();
        wallGenerator.Initialize(wallHeightAdditional, gameAreaRectTransform);
    }
}
