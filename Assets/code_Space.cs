using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code_Space : MonoBehaviour
{

    public Material mat;
    public Vector2 sb;
    public float bx = 0;
    float by = 0;
    float sy = 0;
    float ex = 0;   //rever nomes --> a,b,c
    public bool sg = true;
    float velo = 0.005f;
    bool shoot = false;
    bool invert;

    // Start is called before the first frame update
    void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            bx -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            bx += velo;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            shoot = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            shoot = false;
            by = 0;
        }
    }

    public void SG()
    {
        sg = true;
    }

    private void OnPostRender()
    {
        if (sg)
        {
            Ship();
            Bars();
            Enime();
            if (shoot)
                Shoot_ship();
        }
        Collision_Bars();
    }
    void Ship()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(bx - 1, by - 3, 0);
        GL.Vertex3(bx + 1, by - 3, 0);
        GL.Vertex3(bx + 1, by - 5, 0);
        GL.Vertex3(bx - 1, by - 5, 0);

        GL.End();
        GL.PopMatrix();
    }

    void Enime()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.yellow);
        //rever lógica de pontos para a movimentação!!!

        GL.Vertex3(ex - 6.5f, by + 3.5f , 0);
        GL.Vertex3(ex - 5.5f, by + 3.5f, 0);
        GL.Vertex3(ex -6, by + 2.5f, 0);
                   
        GL.Vertex3(ex + 6.5f, by + 3.5f, 0);
        GL.Vertex3(ex + 5.5f, by + 3.5f, 0);
        GL.Vertex3(ex + 6, by + 2.5f, 0);
                 
        GL.Vertex3(ex - 0.5f, by + 3.5f, 0);
        GL.Vertex3(ex + 0.5f, by + 3.5f, 0);
        GL.Vertex3(ex, by + 2.5f, 0);

        GL.End();
        GL.PopMatrix();
    }

    void Shoot_ship()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        GL.Vertex3(bx - 0.1f, sy - 3, 0);
        GL.Vertex3(bx + 0.1f, sy - 3, 0);
        GL.Vertex3(bx + 0.1f, sy - 3.4f, 0);
        GL.Vertex3(bx - 0.1f, sy - 3.4f, 0);

        GL.End();
        GL.PopMatrix();
        Move_shoot();
    }

    void Bars()
    {
        //são 3 barreiras

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);
        
        GL.Vertex3(bx - 6.5f, by, 0);
        GL.Vertex3(bx - 3.5f, by, 0);
        GL.Vertex3(bx - 3.5f, by - 2, 0);
        GL.Vertex3(bx - 6.5f, by - 2, 0);

        GL.Vertex3(bx + 6.5f, by, 0);
        GL.Vertex3(bx + 3.5f, by, 0);
        GL.Vertex3(bx + 3.5f, by - 2, 0);
        GL.Vertex3(bx + 6.5f, by - 2, 0);

        GL.Vertex3(bx - 1.5f, by, 0);
        GL.Vertex3(bx + 1.5f, by, 0);
        GL.Vertex3(bx + 1.5f, by - 2, 0);
        GL.Vertex3(bx - 1.5f, by - 2, 0);

        GL.End();
        GL.PopMatrix();

    }

    void Collision_Bars()
    {

    }

    void Move_shoot()
    {
        sy += velo;
    }

    void Move()
    {
        if (mGL > sb.x * (-1) && invert)
        {
            mGR -= velo;
            mGL -= velo;
        }
        else
        {
            invert = false;
        }
        if (mGR < sb.x && !invert)
        {
            mGR += velo;
            mGL += velo;
        }
        else
        {
            invert = true;
        }

    }
}
