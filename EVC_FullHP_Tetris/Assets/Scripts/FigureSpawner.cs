using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour {

    GameManager GM;

    GameObject cube1, cube2, cube3, cube4;

    void Awake()
    {
        GM = GetComponent<GameManager>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        int figureType = Random.Range(1, 8);
        Vector3 startVector = new Vector3(GameManager.fieldSizeX / 2, GameManager.fieldSizeY, 1);
        GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube1.transform.position = new Vector3(startVector.x, startVector.y, 0);
        cube1.GetComponent<MeshRenderer>().material = GM.figureMat;
        cube1.AddComponent<TetrisCube>();
        GM.activeCubes[0] = cube1.GetComponent<TetrisCube>();
        
        // Фигура типа i
        if (figureType == 1)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 2, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 3, 0), Quaternion.identity);
        }

        // Фигура типа j
        if (figureType == 2)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y + 1, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y + 2, 0), Quaternion.identity);
        }

        // Фигура типа l
        if (figureType == 3)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 2, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y, 0), Quaternion.identity);
        }

        // Фигура типа o
        if (figureType == 4)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x - 1, startVector.y + 1, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x - 1, startVector.y, 0), Quaternion.identity);
        }

        // Фигура типа s
        if (figureType == 5)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y + 1, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x - 1, startVector.y, 0), Quaternion.identity);
        }

        // Фигура типа t
        if (figureType == 6)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x - 1, startVector.y, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y, 0), Quaternion.identity);
        }

        // Фигура типа z
        if (figureType == 7)
        {
            cube2 = Instantiate(cube1, new Vector3(startVector.x - 1, startVector.y + 1, 0), Quaternion.identity);
            cube3 = Instantiate(cube1, new Vector3(startVector.x, startVector.y + 1, 0), Quaternion.identity);
            cube4 = Instantiate(cube1, new Vector3(startVector.x + 1, startVector.y, 0), Quaternion.identity);
        }

        GM.activeCubes[1] = cube2.GetComponent<TetrisCube>();
        GM.activeCubes[2] = cube3.GetComponent<TetrisCube>();
        GM.activeCubes[3] = cube4.GetComponent<TetrisCube>();
    }
}
