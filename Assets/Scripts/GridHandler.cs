using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridHandler {
    private int row;
    public int[,] grid;

    public GridHandler(int _row) {
        row = _row;
        bool solvable = false;
        while(!solvable) { // shuffle grid until you find a combination that is solvable
            initGrid();
            solvable = IsSolvable();
        }
    }

    private void initGrid() {
        grid = new int[row,row];
        Random rnd = new Random();
        // generate an array in range 0, row^2, then shuffle it
        var array = Enumerable.Range(0, row*row).OrderBy(c => Random.Range(0, row*row)).ToArray(); 

        // fill grid matrix with the shuffled array
        for(int i = 0; i < row; i++)
            for(int j = 0; j < row; j++)
                grid[i,j] = array[i*row + j];
    }

    // rotate N*N matrix clockwise
    public int[,] RotateGrid() {
        int[,] ret = new int[row, row];

        for (int i = 0; i < row; ++i) {
            for (int j = 0; j < row; ++j) {
                ret[i, j] = grid[row - j - 1, i];
            }
        }
        return ret;
    }

    // check if the current shuffle is solvable
    private bool IsSolvable() {
        int[] flatGrid = new int[row * row];

        for (int i = 0; i < row; i++)
            for (int j = 0; j < row; j++)
                flatGrid[i * row + j] = grid[i, j];

        int parity = 0;
        int row2 = 0; // the current row we are on
        int blankRow = 0; // the row with the blank tile

        for (int i = 0; i < flatGrid.Length; i++) {
            if (i % row == 0) { // advance to next row
                row2++;
            }
            if (flatGrid[i] == 0) { // the blank tile
                blankRow = row2; // save the row on which encountered
                continue;
            }
            for (int j = i + 1; j < flatGrid.Length; j++) {
                if (flatGrid[i] > flatGrid[j] && flatGrid[j] != 0) {
                    parity++;
                }
            }
        }

        if (row % 2 == 0) { // even grid
            if (blankRow % 2 == 0) { // blank on odd row; counting from bottom
                return parity % 2 == 0;
            } else { // blank on even row; counting from bottom
                return parity % 2 != 0;
            }
        } else { // odd grid
            return parity % 2 == 0;
        }
    }

    public bool isValidMove(int blockName, SwipeDirection dir) {
        int oldRow = 0;
        int oldCol = 0;
        int newRow = 0;
        int newCol = 0;

        for(int i = 0; i < row; i++) // search for the position of the block in the grid
            for(int j = 0; j < row; j++)
                if(blockName ==  grid[i,j]) {
                    oldRow = i;
                    oldCol = j;
                }

        // find new position using swipe direction
        if (dir == SwipeDirection.Up) {
            newRow = oldRow - 1;
            newCol = oldCol;
        }

        else if (dir == SwipeDirection.Down) {
            newRow = oldRow + 1;
            newCol = oldCol;
        }

        else if (dir == SwipeDirection.Left) {
            newCol = oldCol - 1;
            newRow = oldRow;
        }

        else {
            newCol = oldCol + 1;
            newRow = oldRow;
        }

        // check if the tile is moving outside the grid
        if (newRow < 0 || newCol < 0 || newRow > row - 1 || newCol > row - 1) { 
            return false;
        }
        
        // check if the tile is moving in the empty space
        if (grid[newRow,newCol] != 0) { 
            return false;
        }

        // if the move is valid, move the tile
        grid[newRow, newCol] =  blockName;
        grid[oldRow, oldCol] = 0;

        return true;
    }

    public bool isWon() { 
        if(grid[row-1,row-1] != 0) // check if the last element is zero
            return false;

        for(int i = 0; i < row; i++)
            for(int j = 0; j < row; j++) {
                if(i != row-1 && j != row-1) // don't check the last element again
                    if(grid[i,j] != i*row+j+1)
                        return false;
            }

        return true;
    }

    public override string ToString() {
        var stringGrid = "";
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < row; j++)
                stringGrid = stringGrid + " " + grid[i,j];
            stringGrid += "\n";
        }

        return stringGrid;
    }

    
}
