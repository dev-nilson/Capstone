using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UnityEngine;
using static GameUtilities;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class GridManager : MonoBehaviour
{
    public static GameObject[,] Grid;
    public static Coordinates selectedTile;

    //For drag drop on the players
    public GameObject selectedObject;


    GameController gameController;

    int Col = 5, Row = 5;
    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject tile4;
    public GameObject tile5;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;

    public GameObject player1prefab;
    public GameObject player2prefab;

    public GameObject parent;
    public GameObject levelParent;

    //stores the ending location of the game piece
    Vector3 endLocation;
    //stores the starting location of the game piece
    Vector3 startLocation;

    float startPlayerLoc = 7f;
    float endPlayerLoc = .7f;


    //these are the objects that were instantiated
    GameObject player1Instance;
    GameObject player2Instance;

    int playerNum;

    GameObject gridSpace;
    GameObject child;

    Color Original;
    Color Highlight = new Color(105f, 58f, 48f);

    //get GameObject’s material and color
    MeshRenderer Renderer;

    void Start()
    {
        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();
        //get original color of the GameObject
        Original = Renderer.material.color;
    }

    public void createBoard(int[,] temp)
    {
        Debug.Log("I created a board");
        Grid = new GameObject[Col, Row];
        System.Random rnd = new System.Random();

        //place all tiles
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                int tileNum = rnd.Next(1, 6);   // creates a number between 1 and 5
                if (tileNum == 1)
                {
                    gridSpace = Instantiate(tile1, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 2)
                {
                    gridSpace = Instantiate(tile2, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 3)
                {
                    gridSpace = Instantiate(tile3, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 4)
                {
                    gridSpace = Instantiate(tile4, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 5)
                {
                    gridSpace = Instantiate(tile5, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                gridSpace.name = (i + " " + j);
                gridSpace.transform.parent = parent.transform;
                Grid[i, j] = gridSpace;
            }
        }

        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    //This is used once at the beginning of the game when the players are first placed
    public void placePlayer(int[,] board, Coordinates location, Player P1, Player P2)
    {
        Coordinates[] P1pawns = P1.GetPlayerCoordinates();
        Coordinates loc = new Coordinates(location.X, location.Y);
        if (P1pawns.Contains(loc))
        {
            playerNum = 1;
            player1Instance = Instantiate(player1prefab, startLocation, Grid[location.X, location.Y].transform.rotation);
            placePlayerAnimation(location);

            player1Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player1Instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            player1Instance.transform.SetParent(Grid[location.X, location.Y].transform);

            Debug.Log(player1Instance.transform.parent);
        }
        else //if (P2pawns.Contains(loc))
        {
            playerNum = 2;
            player2Instance = Instantiate(player2prefab, Grid[location.X, location.Y].transform.position, Grid[location.X, location.Y].transform.rotation);
            placePlayerAnimation(location);

            player2Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player2Instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            player2Instance.transform.parent = Grid[location.X, location.Y].transform;
        }
    }

    public void movePlayer(Coordinates curLoc, Coordinates newLoc, Player P1, Player P2)
    {
        Coordinates[] P1pawns = P1.GetPlayerCoordinates();
        Coordinates[] P2pawns = P2.GetPlayerCoordinates();

        Coordinates originalLocation = new Coordinates(curLoc.X, curLoc.Y);
        Coordinates newLocation = new Coordinates(newLoc.X, newLoc.Y);

        startLocation = new Vector3(Grid[curLoc.X, curLoc.Y].transform.position.x, Grid[curLoc.X, curLoc.Y].transform.position.z, Grid[curLoc.X, curLoc.Y].transform.position.z);
        endLocation = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, .7f, Grid[newLoc.X, newLoc.Y].transform.position.z);

        GameObject originalPlayer = Grid[originalLocation.X, originalLocation.Y];
        GameObject newPlayer = Grid[newLocation.X, newLocation.Y].gameObject;

        //loop through each child of the tile that was selected to find the alien (instead of the pyramid levels)
        foreach (Transform child in originalPlayer.transform)
        {
            //this gets the child with the "Alien" tag and sets it to the original player
            if (child.tag == "Alien")
            {
                child.transform.parent = newPlayer.transform;
                Debug.Log("Child name: " + child.gameObject.name);
                originalPlayer = child.gameObject;
                Debug.Log("original player name: " + originalPlayer.name);
            }
        }

        //this sets the alien that was selected to the end location
        originalPlayer.transform.position = endLocation;

        //Debug.Log("original player name: " + originalPlayer.name);

        //Debug.Log("current location: " + player1.name);
        //Debug.Log("new location: " + newPlayer.name);


        //Figure out which player you are moving so that you can set the right prefab
        if (P1pawns.Contains(newLocation))
        {
            //change the parent
            //player1.transform.parent = newPlayerLoc.gameObject.transform;

            /*var player1Instance = Instantiate(player1prefab, newPlayer.transform.position, newPlayer.transform.rotation);
            player1Instance.transform.position = new Vector3(newPlayer.transform.position.x, .7f, newPlayer.transform.position.z);
            player1Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player1Instance.transform.localScale = new Vector3(2.5f, 2f, 2.5f);*/
        }
        else if (P2pawns.Contains(newLocation))
        {
            //change the parent
            //playerPlaced.transform.parent = originalPlayer.transform.parent;

            /*var player2Instance = Instantiate(player2prefab, newPlayer.transform.position, newPlayer.transform.rotation);
            player2Instance.transform.position = new Vector3(newPlayer.transform.position.x, .7f, newPlayer.transform.position.z);
            player2Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player2Instance.transform.localScale = new Vector3(2.5f, 2f, 2.5f);*/
        }
    }

    public void buildLevel(int[,] levelsOnBoard, Coordinates newLoc)
    {
        float levelHeight ;
        if (levelsOnBoard[newLoc.X, newLoc.Y] == 1)
        {
            child = Instantiate(level1, Grid[newLoc.X, newLoc.Y].transform.position, Grid[newLoc.X, newLoc.Y].transform.rotation);
            levelHeight = .7f;
            placeLevelAnimation(newLoc, levelHeight);

            child.transform.parent = Grid[newLoc.X, newLoc.Y].transform;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
            //child.transform.position = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, .70f, Grid[newLoc.X, newLoc.Y].transform.position.z);
            child.transform.localScale = new Vector3(.15f, .5f, .15f);
        }
        else if (levelsOnBoard[newLoc.X, newLoc.Y] == 2)
        {
            child = Instantiate(level2, Grid[newLoc.X, newLoc.Y].transform.position, Grid[newLoc.X, newLoc.Y].transform.rotation);
            levelHeight = 1.5f;
            placeLevelAnimation(newLoc, levelHeight);

            child.transform.parent = Grid[newLoc.X, newLoc.Y].transform;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
            //child.transform.position = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, 1.5f, Grid[newLoc.X, newLoc.Y].transform.position.z);
            child.transform.localScale = new Vector3(.27f, 1f, .27f);
        }
        else if (levelsOnBoard[newLoc.X, newLoc.Y] == 3)
        {
            child = Instantiate(level3, Grid[newLoc.X, newLoc.Y].transform.position, Grid[newLoc.X, newLoc.Y].transform.rotation);
            levelHeight = 2.25f;
            placeLevelAnimation(newLoc, levelHeight);

            child.transform.parent = Grid[newLoc.X, newLoc.Y].transform;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
            //child.transform.position = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, 2.25f, Grid[newLoc.X, newLoc.Y].transform.position.z);
            child.transform.localScale = new Vector3(.072f, .35f, .072f);
        }
        else if (levelsOnBoard[newLoc.X, newLoc.Y] == 4)
        {
            child = Instantiate(level4, Grid[newLoc.X, newLoc.Y].transform.position, Grid[newLoc.X, newLoc.Y].transform.rotation);
            levelHeight = 2.75f;
            placeLevelAnimation(newLoc, levelHeight);

            child.transform.parent = Grid[newLoc.X, newLoc.Y].transform;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
            //child.transform.position = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, 2.75f, Grid[newLoc.X, newLoc.Y].transform.position.z);
            child.transform.localScale = new Vector3(.08f, .23f, .08f);
        }
    }

    /*public void clearBoard()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Alien").Length);
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach(GameObject go in aliens)
        {
            Destroy(go);
        }
        //Destroy(GameObject.FindWithTag("Alien"));
    }*/

    public static GameObject getBoardTile(int row, int col)
    {
        return Grid[row, col];
    }

    public static Coordinates getSelectedTile()
    {
        return selectedTile;
    }

    public void highlightValidTiles(List<Coordinates> locs)
    {
        for (var i = 0; i < Row; ++i)
        {
            for (var j = 0; j < Col; ++j)
            {
                if (locs.Contains(new Coordinates(i, j)))
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    //Color32 newColor = new Color32(0, 200, 26, 200);
                    //Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", newColor);
                    //Grid[i, j].GetComponent<Renderer>().ma

                }
            }
        }
    }
    /*public void highlightValidTiles(int[,] temp)
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                if (temp[i, j] == 0)
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }
            }
        }
    }*/

    public void unhighlightTiles(List<Coordinates> locs)
    {
        for (var i = 0; i < Row; ++i)
        {
            for (var j = 0; j < Col; ++j)
            {
                if (locs.Contains(new Coordinates(i, j)))
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Original);
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1") || true)
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Col; j++)
                {
                    if (Grid[i, j].transform.position == transform.position)
                    {
                        int x = Int32.Parse(Grid[i, j].name.Split(' ')[0]);
                        int y = Int32.Parse(Grid[i, j].name.Split(' ')[1]);

                        selectedTile = new Coordinates(x, y);
                        //Debug.Log(x + " " + y);                  
                    }
                }
            }

            /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Debug.Log("mouse x: " + mousePos.x);
            Debug.Log("mouse y: " + mousePos.y);

            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                Debug.Log("Selected gameobject tag: " + selectedObject.gameObject.tag);
            }*/

        }
    }

    void placePlayerAnimation(Coordinates location)
    {
        StartCoroutine(placePlayerDelay(location));
    }

    IEnumerator placePlayerDelay(Coordinates location)
    {
        StorePhases();
        DisablePhases();

        startLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, 5f, Grid[location.X, location.Y].transform.position.z);
        endLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, .7f, Grid[location.X, location.Y].transform.position.z);

        for (float i = startPlayerLoc; i >= endPlayerLoc; i -= .3f)
        {
            endLocation.y = i;
            if (playerNum == 1)
                player1Instance.transform.position = endLocation;
            else
                player2Instance.transform.position = endLocation;
            yield return new WaitForSeconds(.01f);
        }

        RestorePhases();
        //Debug.Log(CanPlacePawn());
    }

    void placeLevelAnimation(Coordinates location, float levelHeight)
    {
        StartCoroutine(placeLevelDelay(location, levelHeight));
    }

    IEnumerator placeLevelDelay(Coordinates location, float levelHeight)
    {
        StorePhases();
        DisablePhases();

        startLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, 7f, Grid[location.X, location.Y].transform.position.z);
        endLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, levelHeight, Grid[location.X, location.Y].transform.position.z);

        for (float i = startLocation.y; i >= levelHeight; i -= .2f)
        {
            endLocation.y = i;
            child.transform.position = endLocation;
            yield return new WaitForSeconds(.001f);
        }

        yield return new WaitForSeconds(.5f);
        RestorePhases();
        //Debug.Log(CanPlacePawn());
    }

}



