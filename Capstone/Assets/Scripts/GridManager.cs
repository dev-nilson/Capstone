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

    public GameObject scribePrefab;
    public GameObject workerPrefab;
    public GameObject pharoahPrefab;
    public GameObject peasantPrefab;

    GameObject player1Prefab;
    GameObject player2Prefab;

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

    //This is for disabling the rotating board before the players are placed
    public GameObject disabledRight;
    public GameObject disabledLeft;

    //needed for checking when to allow the board to rotate
    int count = 0;

    void Start()
    {
        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();
        //get original color of the GameObject
        Original = Renderer.material.color;
    }

    public void createBoard(int[,] temp)
    {
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

    void setPlayerPrefab()
    {
        if (getP1avatar() == PlayerAvatar.PEASANT) player1Prefab = peasantPrefab;
        else if (getP1avatar() == PlayerAvatar.PHAROAH) player1Prefab = pharoahPrefab;
        else if (getP1avatar() == PlayerAvatar.SCRIBE) player1Prefab = scribePrefab;
        else if (getP1avatar() == PlayerAvatar.WORKER) player1Prefab = workerPrefab;

        if (getP2avatar() == PlayerAvatar.PEASANT) player2Prefab = peasantPrefab;
        else if (getP2avatar() == PlayerAvatar.PHAROAH) player2Prefab = pharoahPrefab;
        else if (getP2avatar() == PlayerAvatar.SCRIBE) player2Prefab = scribePrefab;
        else if (getP2avatar() == PlayerAvatar.WORKER) player2Prefab = workerPrefab;
    }

    //This is used once at the beginning of the game when the players are first placed
    public void placePlayer(int[,] board, Coordinates location, Player P1, Player P2)
    {
        Coordinates[] P1pawns = P1.GetPlayerCoordinates();
        Coordinates loc = new Coordinates(location.X, location.Y);

        disabledRight.SetActive(true);
        disabledLeft.SetActive(true);

        setPlayerPrefab();

        if (P1pawns.Contains(loc))
        {
            playerNum = 1;
            player1Instance = Instantiate(player1Prefab, startLocation, Grid[location.X, location.Y].transform.rotation);
            placePlayerAnimation(location);

            player1Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player1Instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            player1Instance.transform.SetParent(Grid[location.X, location.Y].transform);

            Debug.Log(player1Instance.transform.parent);

            count++;
        }
        else //if (P2pawns.Contains(loc))
        {
            playerNum = 2;
            player2Instance = Instantiate(player2Prefab, Grid[location.X, location.Y].transform.position, Grid[location.X, location.Y].transform.rotation);
            placePlayerAnimation(location);

            player2Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            player2Instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            player2Instance.transform.parent = Grid[location.X, location.Y].transform;

            count++;
        }

        //Make sure all four players have been placed before you allow the board to rotate
        if(count == 4)
        {
            //Turn off the disabled rotating arrow images and that allows the user to rotate the board
            disabledRight.SetActive(false);
            disabledLeft.SetActive(false);
        }
        
    }

    public void movePlayer(Coordinates curLoc, Coordinates newLoc, Player P1, Player P2, GameBoard board)
    {
        // get the new object position
        float curLevelHeight;
        float newLevelHeight;

        if (board.GetHeights()[curLoc.X, curLoc.Y] == 0) curLevelHeight = 0.7f;
        else if (board.GetHeights()[curLoc.X, curLoc.Y] == 1) curLevelHeight = 2.0f;
        else if (board.GetHeights()[curLoc.X, curLoc.Y] == 2) curLevelHeight = 2.75f;
        else curLevelHeight = 3.25f;

        if (board.GetHeights()[newLoc.X, newLoc.Y] == 0) newLevelHeight = 0.7f;
        else if (board.GetHeights()[newLoc.X, newLoc.Y] == 1) newLevelHeight = 2.0f;
        else if (board.GetHeights()[newLoc.X, newLoc.Y] == 2) newLevelHeight = 2.75f;
        else newLevelHeight = 3.25f;

        startLocation = new Vector3(Grid[curLoc.X, curLoc.Y].transform.position.x, curLevelHeight, Grid[curLoc.X, curLoc.Y].transform.position.z);
        endLocation = new Vector3(Grid[newLoc.X, newLoc.Y].transform.position.x, newLevelHeight, Grid[newLoc.X, newLoc.Y].transform.position.z);

        // get the correct game object
        Coordinates originalLocation = new Coordinates(curLoc.X, curLoc.Y);
        Coordinates newLocation = new Coordinates(newLoc.X, newLoc.Y);

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
        //originalPlayer.transform.position = endLocation;

        int heightChange = board.GetHeights()[newLoc.X, newLoc.Y] - board.GetHeights()[curLoc.X, curLoc.Y];
        movePlayerAnimation(originalPlayer, heightChange);
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

    public static void clearSelectedTile()
    {
        selectedTile = new Coordinates();
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
        PauseGame();
        GameBoardScreen.DisableButtons();
        RotateMainCamera.DisableRotation();

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

        PlayGame();
        GameBoardScreen.EnableButtons();
        RotateMainCamera.EnableRotation();
    }

    void placeLevelAnimation(Coordinates location, float levelHeight)
    {
        StartCoroutine(placeLevelDelay(location, levelHeight));
    }

    IEnumerator placeLevelDelay(Coordinates location, float levelHeight)
    {
        PauseGame();
        GameBoardScreen.DisableButtons();
        RotateMainCamera.DisableRotation();

        startLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, 7f, Grid[location.X, location.Y].transform.position.z);
        endLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, levelHeight, Grid[location.X, location.Y].transform.position.z);

        for (float i = startLocation.y; i >= levelHeight; i -= .2f)
        {
            endLocation.y = i;
            child.transform.position = endLocation;
            yield return new WaitForSeconds(.001f);
        }

        //This sets a delay after a player builds their level
        yield return new WaitForSeconds(1f);
        PlayGame();
        GameBoardScreen.EnableButtons();
        RotateMainCamera.EnableRotation();
        //Debug.Log(CanPlacePawn());
    }

    void movePlayerAnimation(GameObject player, int heightChange)
    {
        Debug.Log("move player animation called");
        PauseGame();
        RotateMainCamera.DisableRotation();

        StartCoroutine(movePlayerDelay(player, heightChange));
    }

    IEnumerator movePlayerDelay(GameObject player, int heightChange)
    {
        Debug.Log("move player coroutine called");


        float leapHeight = endLocation.y + 1.0f;
        int h_start = 0;
        int h_end = heightChange; //0, 1, -1, -2, -3
        float h_half = Math.Max(h_start, h_end) + 1.0f;
        int i = 20;

        float h_cur;

        for (float n = 0.0f; n < 1.0f; n += 1.0f/i)
        {
            h_cur = h(n, h_half, h_end);

            float x = startLocation.x + n * (endLocation.x - startLocation.x);
            float z = startLocation.z + n * (endLocation.z - startLocation.z);
            float y = startLocation.y + h_cur * (leapHeight - Math.Min(startLocation.y, endLocation.y));

            //0: 0.7f
            //1: 2.0f
            //2: 2.75f
            //3: 3.25f

            Debug.Log(h_cur + " --> " + y);
            player.transform.position = new Vector3(x, y, z);
            yield return new WaitForSeconds(0.05f);
        }
        player.transform.position = endLocation;

        /* THIS IS GONNA GET REPLACED -- JUST MAKES PIECES MOVE */
        //int iters = 100, count = 0;
        //float x_step = (endLocation.x - startLocation.x) / iters;
        //float y_step = (endLocation.y - startLocation.y) / iters;
        //float z_step = (endLocation.z - startLocation.z) / iters;
        //Vector3 steps = new Vector3(x_step, y_step, z_step);
        //for (Vector3 curLocation = startLocation; curLocation != endLocation && count <= iters; )
        //{
        //    if (Math.Abs(endLocation.x - curLocation.x) < 0.1f && Math.Abs(endLocation.y - curLocation.y) < 0.1f && Math.Abs(endLocation.z - curLocation.z) < 0.1f)
        //        curLocation = endLocation;
        //    else
        //    {
        //        curLocation = curLocation + 2*steps;

        //        player.transform.position = curLocation;
        //        yield return new WaitForSeconds(.01f);
        //    }

        //    ++count;
        //}

        PlayGame();
        RotateMainCamera.EnableRotation();
    }

    private float h(float x, float h_half, int h_end)
    {
        return (2.0f * h_end - 4.0f * h_half) * (float)Math.Pow(x, 2.0f) + (4.0f * h_half - h_end) * x;
    }
}



