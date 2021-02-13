using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallBlocks { baseWallBlock, spikeWallBlock, slimeWallBlock}

[System.Serializable]
public class WallSpawnScenario
{

    [SerializeField] List<WallBlocks> leftWallScenario;
    [SerializeField] List<WallBlocks> rightWallScenario;

    public WallSpawnScenario(int scenariolLength)
    {
        leftWallScenario = new List<WallBlocks>(new WallBlocks[scenariolLength]);
        rightWallScenario = new List<WallBlocks>(new WallBlocks[scenariolLength]);
    }
}

[System.Serializable]
public class ScenariosList
{
    [SerializeField] public List<WallSpawnScenario> ListOfScenarios { get; private set; }

    public ScenariosList(int scenariolLength)
    {
        ListOfScenarios = new List<WallSpawnScenario>
        {
            new WallSpawnScenario(scenariolLength)
        };
    }

}