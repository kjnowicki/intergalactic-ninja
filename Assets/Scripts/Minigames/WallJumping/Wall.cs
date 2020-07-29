using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private List<BaseWallBlock> wallBlocks = new List<BaseWallBlock>();
    private Vector2 newBlockPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(Vector2 intialPosition)
    {
        newBlockPosition = intialPosition;
    }

    public void AddWallBlockOnTop(BaseWallBlock wallBlock)
    {
        BaseWallBlock gameObject = Instantiate(wallBlock, newBlockPosition, Quaternion.identity, transform);
        wallBlocks.Add(wallBlock);
    }
}
