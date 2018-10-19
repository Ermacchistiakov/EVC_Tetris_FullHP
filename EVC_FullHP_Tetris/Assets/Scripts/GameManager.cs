using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int fieldSizeX = 10;
    public static int fieldSizeY = 20;
    public static GameManager GM;
    public static int score = 0;
    public Text scoreText;

    public Canvas mainMenu;
    public Canvas recordsMenu;
    public Canvas nameEntering;
    public Text enteredName;
    public bool gaming = false;

    public Material stakanMat;
    public Material fieldMat;
    public Material figureMat;
    public SoundManager sm;
    public float timer = 0f;
    float timeCoefficient = 4f; // от 4f до 18f

    public TetrisCube[] activeCubes = new TetrisCube[3];
    public static TetrisCube[,] map = new TetrisCube[fieldSizeX, fieldSizeY + 3];

	// Use this for initialization
	void Start() 
    {
        scoreText.text = "Score: 0";
        GM = this;
        StakanBuilding();
        //GetComponent<FigureSpawner>().Spawn();
    }

    void Awake()
    {
        recordsMenu.enabled = false;
        nameEntering.enabled = false;
    }

    public void newGame()
    {
        mainMenu.enabled = false;
        recordsMenu.enabled = false;
        nameEntering.enabled = false;
        score = 0;
        scoreText.text = "Score: 0";
        foreach (TetrisCube cubik in activeCubes)
        {
            if (cubik != null) Destroy(cubik.gameObject);
        }
        foreach (TetrisCube cubik in map)
        {
            if (cubik != null) Destroy(cubik.gameObject);
        }
        map = new TetrisCube[fieldSizeX, fieldSizeY + 3];
        GetComponent<FigureSpawner>().Spawn();
        gaming = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (gaming == true)
        {
            Timing();
        }
	}

    void Timing()
    {
        if (timer >= 1f)
        {
            timer = 0f;

            StepCubes();
        }
        if (score <= 100) timer += Time.deltaTime * (timeCoefficient + score / 10f);
        else timer += Time.deltaTime * (timeCoefficient + 10f);
    }
    // Игровой шаг
    void StepCubes()
    {
        if (TestPosY() == true)
        {
            foreach (TetrisCube cubik in activeCubes)
            {
                cubik.Step();
            }
            sm.PlaySFX("click", 0.07f);
        }
        else
        {
            foreach (TetrisCube cubik in activeCubes) cubik.Deactivate();
            sm.PlaySFX("brick", 1f);
            RowChecker();
            GetComponent<FigureSpawner>().Spawn();
        }
    }
    // Проверка на возможность смещения фигуры вниз
    public bool TestPosY()
    {
        bool answer = true;
        for (int i = 0; i < 4; i++)
        {
            int xpos = (int)activeCubes[i].transform.position.x;
            int ypos = (int)activeCubes[i].transform.position.y;
            if (activeCubes[i].transform.position.y < 1 || map [xpos, ypos-1] != null) answer = false;
        }
        return answer;
    }
    // Проверка на возможность смещения фигуры влево
    public bool TestPosLeft()
    {
        bool answer = true;
        for (int i = 0; i < 4; i++)
        {
            int xpos = (int)activeCubes[i].transform.position.x;
            int ypos = (int)activeCubes[i].transform.position.y;
            if (activeCubes[i].transform.position.x < 1 || map[xpos - 1, ypos] != null) answer = false;
        }
        return answer;
    }
    // Проверка на возможность смещения фигуры вправо
    public bool TestPosRight()
    {
        bool answer = true;
        for (int i = 0; i < 4; i++)
        {
            int xpos = (int)activeCubes[i].transform.position.x;
            int ypos = (int)activeCubes[i].transform.position.y;
            if (activeCubes[i].transform.position.x > fieldSizeX - 2 || map[xpos + 1, ypos] != null) answer = false;
        }
        return answer;
    }
    // Построение "стакана" из кубов
    void StakanBuilding()
    {
        // Строим стенку игрового поля
        for (int y = 0; y < fieldSizeY; y++)
        {
            for (int x = 0; x < fieldSizeX; x++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, y, 1);
                cube.GetComponent<MeshRenderer>().material = fieldMat;
            }
        }

        // Строим обрамление
        for (int y = -1; y < fieldSizeY; y++)
        {
            for (int x = -1; x < fieldSizeX+1; x++)
            {
                if (x == -1 || x == fieldSizeX || y == -1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x, y, 1);
                    cube.GetComponent<MeshRenderer>().material = stakanMat;
                    Instantiate(cube, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    // Проверка на то, есть ли в стакане полные строки
    void RowChecker()
    {
        for (int i = 0; i < fieldSizeY; i++)
        {
            int cubeCount = 0;
            for (int j = 0; j < fieldSizeX; j++)
            {
                if (map[j, i] != null) cubeCount++;
            }
            if (cubeCount == fieldSizeX) // количество кубиков в строке равно горизонтальной размерности стакана, значит, строка заполнена
            {
                for (int x = 0; x < fieldSizeX; x++) // цикл для удаления всех кубиков строки по очереди
                {
                    Destroy(map[x, i].gameObject);
                    map[x, i] = null;
                }
                // А теперь добавляем очки
                score++;
                sm.PlaySFX("success", 1f);
                scoreText.text = "Score: " + score;
                foreach (TetrisCube cubik in map)
                {
                    if (cubik != null) cubik.Repositioning(i);
                }
                i = -1;
            }
        }
    }
    // Конец раунда
    public void GameOver()
    {
        gaming = false;
        this.GetComponent<ScoreManager>().CompareMyScore(score);
        recordsMenu.enabled = true;
    }

    // Открытие меню с рекордами
    public void OpenRecords()
    {
        mainMenu.enabled = false;
        recordsMenu.enabled = true;
    }

    // Команда при нажатии по кнопке "Accept" после введения имени
    public void NameAccepting()
    {
        if (enteredName.text.Contains("(") == false && enteredName.text.Contains(")") == false && enteredName.text.Contains(";") == false && enteredName.text != null)
        {
            this.GetComponent<ScoreManager>().NameInserting(enteredName.text);
            nameEntering.enabled = false;
        }
    }

    // Поворот фигуры
    public void FigureRotation()
    {
        int[,] cubes_xy = new int[4,2];
        int[,] new_xy = new int[4,2];
        for (int i = 0; i < 4; i++)
        {
            cubes_xy[i,0] = (int)activeCubes[i].transform.position.x;
            cubes_xy[i,1] = (int)activeCubes[i].transform.position.y;
        }
        new_xy[0,0] = cubes_xy[0,0];
        new_xy[0,1] = cubes_xy[0,1];
        bool available = true;
        int xdeviation = 0;
        // Проверяем, являются ли доступными новые координаты кубиков
        for (int i = 1; i < 4; i++)
        {
            new_xy[i, 0] = cubes_xy[0, 0] + xdeviation + (cubes_xy[i, 1] - cubes_xy[0, 1]);
            new_xy[i, 1] = cubes_xy[0, 1] + (cubes_xy[0, 0] - cubes_xy[i, 0]);
            if (new_xy[i, 0] < 0 || new_xy[i, 0] >= fieldSizeX || map[new_xy[i, 0], new_xy[i, 1]] != null) // место для кубика либо выходит за пределы поля, либо занято другим кубиком
            {

                if (new_xy[i, 1] < 0 || new_xy[i, 1] >= fieldSizeY) // не проходит по вертикали - отменяем разворот.
                {
                    available = false;
                    break;
                }

                if (xdeviation > 4 || xdeviation < -4) // если отклонение более 4 единиц от прежней точки, отменяем разворот
                {
                    Debug.Log(xdeviation);
                    sm.PlaySFX("success", 1f);
                    available = false;
                    break;
                }
                    if (xdeviation > 0) // если доп.отклонение больше 0 (ведет вправо)
                    {
                       xdeviation *= -1;        
                    }
                    else if (xdeviation < 0) // если доп.отклонение меньше 0 (ведет влево)
                    {
                       xdeviation = -1 * (xdeviation - 1); // усиливаем отклонение на 1 единицу и меняем направление на правое
                      
                    }
                    if (xdeviation == 0) xdeviation = 1; // если отклонение нулевое, меняем его на 1
                    
                    new_xy[0, 0] = cubes_xy[0, 0] + xdeviation;
                    i = 0; // цикл начинаем сначала, ибо появилось отклонение для всех кубиков
            }  
        }
        if (available == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (map[new_xy[i, 0], new_xy[i, 1]] != null) break;
                activeCubes[i].transform.position = new Vector3(new_xy[i, 0], new_xy[i, 1], activeCubes[i].transform.position.z);
            }
        }
    }
}

