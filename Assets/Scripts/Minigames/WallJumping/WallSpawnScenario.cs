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

    public (WallBlocks, WallBlocks) GetNextBlocks()
    {
        var leftBlock = leftWallScenario[leftWallScenario.Count - 1];
        var rightBlock = leftWallScenario[leftWallScenario.Count - 1];
        leftWallScenario.RemoveAt(leftWallScenario.Count - 1);
        rightWallScenario.RemoveAt(leftWallScenario.Count - 1);
        return (leftBlock, rightBlock);
    } 
}

[System.Serializable]
public class ScenariosList
{
    [SerializeField] private List<WallSpawnScenario> listOfScenarios;

    public ScenariosList(int scenariolLength)
    {
        listOfScenarios = new List<WallSpawnScenario>
        {
            new WallSpawnScenario(scenariolLength)
        };
    }

    public WallSpawnScenario GetRandomScenario()
    {
        System.Random rnd = new System.Random();

        return listOfScenarios[rnd.Next(listOfScenarios.Count)];
    }
}