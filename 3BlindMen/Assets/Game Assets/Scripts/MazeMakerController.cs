using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeMakerController : MonoBehaviour
{
    public GameObject wall;
    public GameObject light;

    const int MAZE_SIZE = 10;
    CellType[, ,] grid = new CellType[MAZE_SIZE, MAZE_SIZE, MAZE_SIZE];
    int openPaths = 0;
    //int remainingSquares = MAZE_SIZE * MAZE_SIZE * MAZE_SIZE;

    List<Vector3> cellsToGenerate;

    enum CellType
    {
        Open, Wall, Nothing, End, Start
    };


    // Use this for initialization
    void Start()
    {
        //Initialize cells queue
        cellsToGenerate = new List<Vector3>();

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
        grid[MAZE_SIZE / 2, MAZE_SIZE / 2, MAZE_SIZE / 2] = CellType.Start;
        cellsToGenerate.Add(new Vector3(MAZE_SIZE / 2, MAZE_SIZE / 2, MAZE_SIZE / 2));
        while (cellsToGenerate.Count > 0)
        {
            rGenerateMaze();
        }

        // generate outer walls
        for (int i = 0; i <= MAZE_SIZE; i++)
        {
            for (int j = 0; j <= MAZE_SIZE; j++)
            {
                Instantiate(wall, new Vector3(0,i, j), Quaternion.identity);
                Instantiate(wall, new Vector3(MAZE_SIZE, i, j), Quaternion.identity);
                Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity);
                Instantiate(wall, new Vector3(i, MAZE_SIZE, j), Quaternion.identity);
                Instantiate(wall, new Vector3(i, j, 0), Quaternion.identity);
                Instantiate(wall, new Vector3(i, j, MAZE_SIZE), Quaternion.identity);
                for (int k = 0; k < MAZE_SIZE; k++)
                {
                    if (i < MAZE_SIZE && j < MAZE_SIZE)
                    {
                        if (grid[i, j, k] == CellType.Nothing)
                        {
                            Instantiate(wall, new Vector3(i, j, k), Quaternion.identity);
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
    void rGenerateMaze()
    {
        Vector3 currentCell = cellsToGenerate[0];
        cellsToGenerate.RemoveAt(0);
        openPaths += 6;
        if (currentCell.x + 1 < MAZE_SIZE - 1)
        {
            createWallOpenRandom(new Vector3(currentCell.x + 1, currentCell.y, currentCell.z));
        }
        if (currentCell.x - 1 > 0)
        {
            createWallOpenRandom(new Vector3(currentCell.x - 1, currentCell.y, currentCell.z));
        }
        if (currentCell.y + 1 < MAZE_SIZE - 1)
        {
            createWallOpenRandom(new Vector3(currentCell.x, currentCell.y + 1, currentCell.z));
        }
        if (currentCell.y - 1 > 0)
        {
            createWallOpenRandom(new Vector3(currentCell.x, currentCell.y - 1, currentCell.z));
        }
        if (currentCell.z + 1 < MAZE_SIZE - 1)
        {
            createWallOpenRandom(new Vector3(currentCell.x, currentCell.y, currentCell.z + 1));
        }
        if (currentCell.z - 1 > 0)
        {
            createWallOpenRandom(new Vector3(currentCell.x, currentCell.y, currentCell.z - 1));
        }
    }

    /// <summary>
    /// Creates the cell and sets it to either a wall or an open space.
    /// </summary>
    /// <param name="cell">Cell.</param>
    void createWallOpenRandom(Vector3 cell)
    {
        if (grid[(int)cell.x, (int)cell.y, (int)cell.z] == CellType.Nothing)
        {
            int randomChoice = (int)(Random.Range(0, 5));
            if (randomChoice == 0 || openPaths == 0)
            {
                grid[(int)cell.x, (int)cell.y, (int)cell.z] = CellType.Open;
                cellsToGenerate.Add(cell);
            }
            else
            {
                grid[(int)cell.x, (int)cell.y, (int)cell.z] = CellType.Wall;
                Instantiate(wall, cell, Quaternion.identity);
                openPaths--;
            }
        }
        else
        {
            openPaths--;
        }
    }
}
