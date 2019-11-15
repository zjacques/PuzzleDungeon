using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMan : MonoBehaviour
{
    public Vector2Int dims;
    public GameObject tile;
    List<List<GameObject>> grid;

    public Text successText;
    GameObject endTile;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        Quaternion tileRot = transform.rotation;
        Vector3 tilePos = transform.position;

        grid = new List<List<GameObject>>();
        for (int i = 0; i < dims.x; i++)
        {
            grid.Add(new List<GameObject>());

            for (int j = 0; j<dims.y; j++)
            {
                if(!(j==dims.y-1 && i==dims.x-1))
                {
                    grid[i].Add(Instantiate(tile, tilePos, tileRot));
                    tilePos.y++;
                }
            }
            tilePos.x++;
            tilePos.y = transform.position.y;
        }
        endTile = GameObject.FindGameObjectWithTag("Goal");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 endPos = endTile.transform.position;
        if(Mathf.RoundToInt(playerPos.x)==Mathf.RoundToInt(endPos.x) && Mathf.RoundToInt(playerPos.y)==Mathf.RoundToInt(endPos.y))
        {
            successText.text = "You Win!";
        }
    }

    void resetGame()
    {

    }

}
