using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {

    bool leftpressed = false;
    bool rightpressed = false;
    bool downpressed = false;
    float presstimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        KeyboardCheck();
        if (leftpressed == true || rightpressed == true || downpressed == true)
        {
            presstimer += 3f * Time.deltaTime;
            if (presstimer >= 1f)
            {
                presstimer = 0.5f;
                if (leftpressed == true) MoveFigureLeft();
                if (rightpressed == true) MoveFigureRight();     
            }
            if (downpressed == true)
            {
                if (GameManager.GM.timer < 0.85f)
                {
                    GameManager.GM.timer = 0.85f;
                }
            }
        }
        else
        {
            presstimer = 0f;
        }
	}
    
    // смещение фигуры влево
    void MoveFigureLeft()
    {
        if (GameManager.GM.TestPosLeft() == true)
        {
            foreach (TetrisCube cubik in GameManager.GM.activeCubes)
            {
                cubik.MoveX(-1);
            }
        }
    }
    // смещение фигуры вправо
    void MoveFigureRight()
    {
        if (GameManager.GM.TestPosRight() == true)
        {
            foreach (TetrisCube cubik in GameManager.GM.activeCubes)
            {
                cubik.MoveX(+1);
            }
        }
    }

    void KeyboardCheck()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftPress();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            leftRelease();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rightPress();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rightRelease();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            downPress();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            downRelease();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotatePress();
        }
    }

    public void leftPress()
    {
        rightpressed = false;
        leftpressed = true;
        MoveFigureLeft();
    }

    public void leftRelease()
    {
        leftpressed = false;
    }

    public void rightPress()
    {
        leftpressed = false;
        rightpressed = true;
        MoveFigureRight();
    }

    public void rightRelease()
    {
        rightpressed = false;
    }

    public void downPress()
    {
        downpressed = true;
        GameManager.GM.timer = 1f;
    }

    public void downRelease()
    {
        downpressed = false;
    }

    public void rotatePress()
    {
        GameManager.GM.FigureRotation();
    }
}
