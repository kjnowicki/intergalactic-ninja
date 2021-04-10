using UnityEngine;

namespace Assets.Scripts.Minigames.NinjaTiles
{
    public class Spaceship : MonoBehaviour
    {
        public void MoveSpaceshipAboveTileAtIndex(int tileIndex)
        {
            Vector3 currentPosition = transform.position;
            int tileShiftedIndex = tileIndex - NinjaTilesGameManager.TileIndexShift;
            transform.position = new Vector3(tileShiftedIndex * NinjaTilesGameManager.ScreenScale.x, currentPosition.y, currentPosition.z);
        }
    }
}