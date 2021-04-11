using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private Transform[] tiles; // tiles prefab ordered from 1 to 15
    private GridHandler gridHandler; // class that hold the tiles matrix
    [SerializeField] private int row; // number of row / columns
    private StopWatchManager sw;
    [SerializeField] private AudioClip tileMoving;
    private AudioSource audioSource;

    void Awake() {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    void Start() {
        sw = GetComponent<StopWatchManager>();
        audioSource = GetComponent<AudioSource>();
        StartGame();
    }

    public void StartGame() {
        sw.Reset();  // reset the stopwatch to 00:00:00
        gridHandler = new GridHandler(row);  // create the grid that hold tiles position
        print(gridHandler);
        DestroyTiles(); // destroy tiles if it is not the first game
        SpawnTiles();  // spawn tiles gameobject
    }

    // Spawn tiles for the first time
    private void SpawnTiles() {
        int[,] rotatedGrid = gridHandler.RotateGrid(); // rotate matrix for spawning tiles correctly

        for(int i = 0; i < row; i++)
            for(int j = 0; j < row; j++) {
                if(rotatedGrid[i,j] != 0) {
                    Instantiate(tiles[rotatedGrid[i, j]-1], new Vector2(i-1.5f, j-4f), Quaternion.identity);
                }
            }
    }

    private void DestroyTiles() {
        Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
        foreach (var tile in tiles)
            Destroy(tile.gameObject);
    }

    private void SwipeDetector_OnSwipe(SwipeData data) {
        // start position is actually end position in the swipeDetector, position is transformed in game coordinates
        Vector3 startPosition = Camera.main.ScreenToWorldPoint (data.EndPosition); 
        RaycastHit2D hit = Physics2D.Raycast((Vector2)startPosition, Vector2.zero); // check if swipe gesture is starting from an object
        if (hit.collider != null) {
            moveBlock(hit.transform, data.Direction);
        }
    }

    private void moveBlock(Transform block, SwipeDirection dir) {
        if(!sw.running) // if timer is already running don't restart it
            sw.Go();

        // if it is not a valid move return, else move block in the swipe direction
        if (!gridHandler.isValidMove(block.GetComponent<Tile>().Number, dir))
            return;

        if(dir == SwipeDirection.Up)
            block.transform.position = new Vector2(block.transform.position.x, block.transform.position.y + 1);
        else if (dir == SwipeDirection.Down)
            block.transform.position = new Vector2(block.transform.position.x, block.transform.position.y - 1);
        else if (dir == SwipeDirection.Left)
            block.transform.position = new Vector2(block.transform.position.x - 1, block.transform.position.y);
        else if (dir == SwipeDirection.Right)
            block.transform.position = new Vector2(block.transform.position.x + 1, block.transform.position.y);

        audioSource.PlayOneShot(tileMoving);

        print("won: " + gridHandler.isWon());
    }

}
