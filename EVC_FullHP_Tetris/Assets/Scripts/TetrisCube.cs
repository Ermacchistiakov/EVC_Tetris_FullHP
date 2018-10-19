using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCube : MonoBehaviour {

    public bool falling = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void Step()
    {
        if (falling == true)
        {
            Vector3 pos = this.transform.position;
            this.transform.position = new Vector3(pos.x, pos.y - 1, pos.z);
        }

    }

    public void Deactivate()
    {
        falling = false;
        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;
        if (y < GameManager.fieldSizeY)
        {
            GameManager.map[x, y] = this;
        }
        else
        {
            if (GameManager.GM.gaming == true) GameManager.GM.GameOver();
            Destroy(this.gameObject);
        }
    }

    public void MoveX(int movx)
    {
        transform.position = new Vector3(transform.position.x + movx, transform.position.y, transform.position.z);
    }

    public void Repositioning(int rowY) // смещение кубиков вниз после уничтожения заполненной строки
    {
        if (transform.position.y > rowY && falling == false)
        {
            int x = (int)this.transform.position.x;
            int y = (int)this.transform.position.y;
            GameManager.map[x, y] = null;
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            GameManager.map[x, y-1] = this;
        }
    }
}
