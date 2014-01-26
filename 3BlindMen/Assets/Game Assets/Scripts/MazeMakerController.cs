using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeMakerController : MonoBehaviour
{
    public GameObject wall;

    const int MAZE_SIZE = 10;
    CellType[, ,] grid = new CellType[MAZE_SIZE, MAZE_SIZE, MAZE_SIZE];
    int openPathsStart = 1;
    int openPathsEnd = 1;


    List<Vector3> cellsToGenerateStart;
    List<Vector3> cellsToGenerateEnd;

    enum CellType
    {
        Open, Wall, Nothing, End, Start
    };


    // Use this for initialization
    void Start()
    {
        //Initialize cells queue
        cellsToGenerateStart = new List<Vector3>();
        cellsToGenerateEnd = new List<Vector3>();

        // Initialize the grid so each cell is Nothing
        for (int i = 0; i < MAZE_SIZE; i++)
        {
            for (int j = 0; j < MAZE_SIZE; j++)
            {
                for (int k = 0; k < MAZE_SIZE; k++)
                {
                    grid[i, j, k] = CellType.Nothing;
                }
            }
        }

        // Set the start point
        grid[1, 1, 1] = CellType.Start;
        grid[MAZE_SIZE - 1, MAZE_SIZE - 1, MAZE_SIZE - 1] = CellType.End;
        cellsToGenerateStart.Add(new Vector3(1, 1, 1));
        cellsToGenerateEnd.Add(new Vector3(MAZE_SIZE - 1, MAZE_SIZE - 1, MAZE_SIZE - 1));
        while (cellsToGenerateStart.Count > 0 || cellsToGenerateEnd.Count > 0)
        {
            if (cellsToGenerateStart.Count > 0)
            {
                rGenerateMaze(cellsToGenerateStart, ref openPathsStart);
            }
            else
            {
                rGenerateMaze(cellsToGenerateEnd, ref openPathsEnd);
            }
        }

        // generate outer walls
        for (int i = 0; i <= MAZE_SIZE; i++)
        {
            for (int j = 0; j <= MAZE_SIZE; j++)
            {
                Network.Instantiate(wall, new Vector3(0,i, j), Quaternion.identity,0);
                Network.Instantiate(wall, new Vector3(MAZE_SIZE, i, j), Quaternion.identity,0);
                Network.Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity,0);
                Network.Instantiate(wall, new Vector3(i, MAZE_SIZE, j), Quaternion.identity,0);
                Network.Instantiate(wall, new Vector3(i, j, 0), Quaternion.identity,0);
                Network.Instantiate(wall, new Vector3(i, j, MAZE_SIZE), Quaternion.identity,0);
                for (int k = 0; k < MAZE_SIZE; k++)
                {
                    if (i < MAZE_SIZE && j < MAZE_SIZE)
                    {
                        if (grid[i, j, k] == CellType.Nothing)
                        {
                            Network.Instantiate(wall, new Vector3(i, j, k), Quaternion.identity, 0);
                            grid[i, j, k] = CellType.Wall;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Recursively generates the maze given a start point.
    /// </summary>
    /// <param name="currentCell">Cell to start.</param>
    void rGenerateMaze(List<Vector3> cellsToGenerate, ref int openPaths)
    {
        Vector3 currentCell = cellsToGenerate[0];
        cellsToGenerate.RemoveAt(0);
        openPaths--;
        if (currentCell.x + 1 < MAZE_SIZE)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x + 1, currentCell.y, currentCell.z), ref openPaths);
        }
        if (currentCell.x - 1 > 0)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x - 1, currentCell.y, currentCell.z), ref openPaths);
        }
        if (currentCell.y + 1 < MAZE_SIZE)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x, currentCell.y + 1, currentCell.z), ref openPaths);
        }
        if (currentCell.y - 1 > 0)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x, currentCell.y - 1, currentCell.z), ref openPaths);
        }
        if (currentCell.z + 1 < MAZE_SIZE)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x, currentCell.y, currentCell.z + 1), ref openPaths);
        }
        if (currentCell.z - 1 > 0)
        {
            createWallOpenRandom(cellsToGenerate, new Vector3(currentCell.x, currentCell.y, currentCell.z - 1), ref openPaths);
        }
    }

    /// <summary>
    /// Creates the cell and sets it to either a wall or an open space.
    /// </summary>
    /// <param name="cell">Cell.</param>
    void createWallOpenRandom(List<Vector3> cellsToGenerate, Vector3 cell, ref int openPaths)
    {
        if (grid[(int)cell.x, (int)cell.y, (int)cell.z] == CellType.Nothing)
        {
            int randomChoice = (int)(Random.Range(0, 5));
            if (randomChoice == 0 || openPaths < 1)
            {
                grid[(int)cell.x, (int)cell.y, (int)cell.z] = CellType.Open;
                cellsToGenerate.Add(cell);
                openPaths++;
            }
            else
            {
                grid[(int)cell.x, (int)cell.y, (int)cell.z] = CellType.Wall;
                Network.Instantiate(wall, cell, Quaternion.identity, 0);
            }
        }
    }
}
