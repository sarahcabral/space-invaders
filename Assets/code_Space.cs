using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code_Space : MonoBehaviour
{

    public Material mat;
    public Vector2 sb;

    float sx = 0;                   //nave
    float[] ex = new float[3];      //inimigo
    float sx2 = 0;                  //tiro nave
    float sy = 0;                   //tiro nave
    float[] by = new float[6];      //barra
    float[] bx = new float[6];      //barra
    float[] eSx = new float[3];     //tiro inimigo
    float[] eSy = new float[3];     //tiro inimigo

    public int[] vida_enemies = new int[3];
    bool[] moving = new bool[3];
    public int vida = 3;
    public bool sg = true;
    float velo = 0.005f;
    float move = 0.0025f;
    bool invert;
    bool space = false;
    bool atirando = false;
    bool[] acertou = new bool[3];


    // Start is called before the first frame update
    void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        vida_enemies[0] = 3;
        vida_enemies[1] = 3;
        vida_enemies[2] = 3;
    }

    // Update is called once per frame
    void Update()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sx -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sx += velo;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            space = true;
            atirando = true;
        }
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    by = 0;
        //}
        Collision_Bars();
        Collision_Enemies();
        Move_enime();
        Shoot_Enemi1();
        Shoot_Enemi2();
        Shoot_Enemi3();

    }

    public void SG()
    {
        sg = true;
    }

    private void OnPostRender()
    {
        if(vida > 0)
        if (sg)
        {
            Ship();
            Bars();
            if (vida_enemies[0] > 0)
            {
                Enime1();
                Shoot_Enemi1();
            }
            if (vida_enemies[1] > 0)
            {
                Enime2();
                Shoot_Enemi2();
            }
            if (vida_enemies[2] > 0)
            { 
                Enime3();
                Shoot_Enemi3();
            }
            Shoot_ship();
        }
        else
        {
            
        }
    }
    void Ship()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.black);

        GL.Vertex3(sx - 1, -5, 0);
        GL.Vertex3(sx , -4, 0);
        GL.Vertex3(sx + 1, -5, 0);

        GL.End();
        GL.PopMatrix();
    }

    void Enime1()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.yellow);

        GL.Vertex3(ex[0] - 6.5f, 3.5f, 0);
        GL.Vertex3(ex[0] - 5.5f, 3.5f, 0);
        GL.Vertex3(ex[0] - 6, 2.5f, 0);

        GL.End();
        GL.PopMatrix();
    }
    void Enime2()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.yellow);

        GL.Vertex3(ex[1] - 0.5f, 3.5f, 0);
        GL.Vertex3(ex[1] + 0.5f, 3.5f, 0);
        GL.Vertex3(ex[1], 2.5f, 0);

        GL.End();
        GL.PopMatrix();
    }
    void Enime3()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.yellow);

        GL.Vertex3(ex[2] + 6.5f, 3.5f, 0);
        GL.Vertex3(ex[2] + 5.5f, 3.5f, 0);
        GL.Vertex3(ex[2] + 6, 2.5f, 0);

        GL.End();
        GL.PopMatrix();
    }
    void Shoot_ship()
    {
        if (!atirando)
        {
            sx2 = sx;
        }

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);

        GL.Vertex3(sx2 - 0.1f, sy - 4, 0);
        GL.Vertex3(sx2 + 0.1f, sy - 4, 0);
        GL.Vertex3(sx2 + 0.1f, sy - 4.4f, 0);
        GL.Vertex3(sx2 - 0.1f, sy - 4.4f, 0);

        GL.End();
        GL.PopMatrix();
        Move_shoot();
    }

    void Bars()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);

        GL.Vertex3(bx[0] - 6.5f, by[0], 0);
        GL.Vertex3(bx[1] - 3.5f, by[0], 0);
        GL.Vertex3(bx[1] - 3.5f, by[1] - 2, 0);
        GL.Vertex3(bx[0] - 6.5f, by[1] - 2, 0);

        GL.Vertex3(bx[2] + 6.5f, by[2], 0);
        GL.Vertex3(bx[3] + 3.5f, by[2], 0);
        GL.Vertex3(bx[3] + 3.5f, by[3] - 2, 0);
        GL.Vertex3(bx[2] + 6.5f, by[3] - 2, 0);

        GL.Vertex3(bx[4] - 1.5f, by[4], 0);
        GL.Vertex3(bx[5] + 1.5f, by[4], 0);
        GL.Vertex3(bx[5] + 1.5f, by[5] - 2, 0);
        GL.Vertex3(bx[4] - 1.5f, by[5] - 2, 0);

        GL.End();
        GL.PopMatrix();

    }
    
    void Shoot_Enemi1()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);

        if (!moving[0])
        {
            eSx[0] = ex[0];
        }
        GL.Vertex3(eSx[0] - 6.1f, eSy[0] + 2.5f, 0);
        GL.Vertex3(eSx[0] - 5.9f, eSy[0] + 2.5f, 0);
        GL.Vertex3(eSx[0] - 6.1f, eSy[0] + 2.1f, 0);
        GL.Vertex3(eSx[0] - 5.9f, eSy[0] + 2.1f, 0);

        GL.End();
        GL.PopMatrix();
        Move_enime_shoot1();
    }
    void Shoot_Enemi2()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        if (!moving[1])
        {
            eSx[1] = ex[1];
        }
        GL.Vertex3(eSx[1] - 0.1f, eSy[1] + 2.5f, 0);
        GL.Vertex3(eSx[1] + 0.1f, eSy[1] + 2.5f, 0);
        GL.Vertex3(eSx[1] + 0.1f, eSy[1] + 2.1f, 0);
        GL.Vertex3(eSx[1] - 0.1f, eSy[1] + 2.1f, 0);

        GL.End();
        GL.PopMatrix();
        Move_enime_shoot2();
    }
    void Shoot_Enemi3()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        if (!moving[2])
        {
            eSx[2] = ex[2];
        }
        GL.Vertex3(eSx[2] + 6.1f, eSy[2] + 2.5f, 0);
        GL.Vertex3(eSx[2] + 5.9f, eSy[2] + 2.5f, 0);
        GL.Vertex3(eSx[2] + 6.1f, eSy[2] + 2.1f, 0);
        GL.Vertex3(eSx[2] + 5.9f, eSy[2] + 2.1f, 0);

        GL.End();
        GL.PopMatrix();
        Move_enime_shoot3();
    }

    void Collision_Bars()
    {
        //Tiros Barra
        if (eSy[0] + 2.1f <= by[0] && eSx[0] - 0.1f >= bx[0] - 6.5f && eSx[0] - 0.1f <= bx[1] - 3.5f)
        {
            if (by[0] > -2)
            {
                bx[0] += 0.025f;
                by[0] -= 0.025f;
            }
            else
            {
                bx[0] = bx[1];
                by[0] = -2;
            }
            eSy[0] = 0;
            moving[0] = false;
            acertou[0] = true;
        }
        else
        {
            acertou[0] = false;
        }

        if(eSy[0] + 2.1f <= by[2] && eSx[0] - 0.1f <= bx[2] + 6.5f && eSx[0] - 0.1f >= bx[3] + 3.5f)
        {
            if (by[2] > -2)
            {
                bx[3] += 0.025f;
                by[2] -= 0.025f;
            }
            else
            {
                bx[3] = bx[2];
                by[2] = -2;
            }
            eSy[0] = 0;
            moving[0] = false;
            acertou[0] = true;
        }
        else
        {
            acertou[0] = false;
        }

        if (eSy[0] + 2.1f <= by[4] && eSx[0] - 0.1f >= bx[4] - 1.5f && eSx[0] - 0.1f <= bx[5] + 1.5f)
        {
            if (by[4] > -2)
            {
                bx[4] += 0.025f;
                by[4] -= 0.025f;
            }
            else
            {
                bx[4] = bx[5];
                by[4] = -2;
            }
            eSy[0] = 0;
            moving[0] = false;
            acertou[0] = true;
        }
        else
        {
            acertou[0] = false;
        }

        //-------------------------------------------------------------------------------------------
        if (eSy[1] + 2.1f <= by[0] && eSx[1] - 6.1f >= bx[0] - 6.5f && eSx[1] - 6.1f <= bx[1] - 3.5f)
        {
            if (by[0] > -2)
            {
                bx[0] += 0.025f;
                by[0] -= 0.025f;
            }
            else
            {
                bx[0] = bx[1];
                by[0] = -2;
            }
            eSy[1] = 0;
            moving[1] = false;
            acertou[1] = true;
        }
        else
        {
            acertou[1] = false;
        }

        if (eSy[1] + 2.1f <= by[4] && eSx[1] - 6.1f >= bx[4] - 1.5f && eSx[1] - 6.1f <= bx[5] + 1.5f)
        {
            if (by[4] > -2)
            {
                bx[4] += 0.025f;
                by[4] -= 0.025f;
            }
            else
            {
                bx[4] = bx[5];
                by[4] = -2;
            }
            eSy[1] = 0;
            moving[1] = false;
            acertou[1] = true;
        }
        else
        {
            acertou[1] = false;
        }

        //-----------------------------------------------------------------------------------------
        if (eSy[2] + 2.1f <= by[2] && eSx[2] + 5.9f <= bx[2] + 6.5f && eSx[2] + 5.9f >= bx[3] + 3.5f)
        {
            if (by[2] > -2)
            {
                bx[3] += 0.025f;
                by[2] -= 0.025f;
            }
            else
            {
                bx[3] = bx[2];
                by[2] = -2;
            }
            eSy[2] = 0;
            moving[2] = false;
            acertou[2] = true;
        }
        else
        {
            acertou[2] = false;
        }

        if (eSy[2] + 2.1f <= by[4] && eSx[2] + 5.9f >= bx[4] - 1.5f && eSx[2] + 5.9f <= bx[5] + 1.5f)
        {
            if (by[4] > -2)
            {
                bx[4] += 0.025f;
                by[4] -= 0.025f;
            }
            else
            {
                bx[4] = bx[5];
                by[4] = -2;
            }
            eSy[2] = 0;
            moving[2] = false;
            acertou[2] = true;
        }
        else
        {
            acertou[2] = false;
        }

        //-----------------------------------------------------------------------------------------
        //TIRO NA NAVE
        if (eSy[0] + 2.1f <= -4 && eSx[0] - 0.1f >= sx - 1 && eSx[0] - 0.1f <= sx + 1)
        {
            vida--;
            eSy[0] = 0;
            moving[0] = false;
            acertou[0] = true;
        }
        else
        {
            acertou[0] = false;
        }
        if (eSy[1] + 2.1f <= -4 && eSx[1] - 6.1f >= sx - 1 && eSx[1] - 6.1f <= sx + 1)
        {
            vida--;
            eSy[1] = 0;
            moving[1] = false;
            acertou[1] = true;
        }
        else
        {
            acertou[1] = false;
        }
        if (eSy[2] + 2.1f <= -4 && eSx[2] + 5.9f >= sx - 1 && eSx[2] + 5.9f <= sx + 1)
        {
            vida--;
            eSy[2] = 0;
            moving[2] = false;
            acertou[2] = true;
        }
        else
        {
            acertou[2] = false;
        }
    }


    void Move_shoot()
    {
        if ((sy - 3) < sb.y && space)
        {
            sy += velo * 3;
        }
        else
        {
            sy = 0;
            space = false;
            atirando = false;
        }

    }

    void Move_enime()
    {
        if ((ex[0] - 6.5f) > sb.x * (-1) && invert)
        {
            ex[0] -= move;
            ex[1] -= move;
            ex[2] -= move;
        }
        else
        {
            invert = false;
        }
        if ((ex[2] + 6.5f) < sb.x && !invert)
        {
            ex[0] += move;
            ex[1] += move;
            ex[2] += move;
        }
        else
        {
            invert = true;
        }
    }

    void Move_enime_shoot1()
    {
        if ((eSy[0] + 2.1f) > sb.y * -1 && !acertou[0])
        {
            eSy[0] -= velo;
            moving[0] = true;
            acertou[0] = false;
        }
        else
        {
            eSy[0] = 0;
            moving[0] = false;
            acertou[0] = true;
        }
    }
    void Move_enime_shoot2()
    {
        if ((eSy[1] + 2.1f) > sb.y * -1 && !acertou[1])
        {
            eSy[1] -= velo;
            moving[1] = true;
            acertou[1] = false;
        }
        else
        {
            eSy[1] = 0;
            moving[1] = false;
            acertou[1] = true;
        }
    }
    void Move_enime_shoot3()
    {
        if ((eSy[2] + 2.1f) > sb.y * -1 && !acertou[2])
        {

            eSy[2] -= velo;
            moving[2] = true;
            acertou[2] = false;
        }
        else
        {
            eSy[2] = 0;
            moving[2] = false;
            acertou[2] = true;
        }
    }


    void Collision_Enemies()
    {
        if (sy - 3 >= 2.5f && sx2 - 0.1f >= ex[0] - 6.5f && sx2 - 0.1f <= ex[0] - 5.5f)
        {
            vida_enemies[0] -= 1;
            sy = 0;
            space = false;
        }
        if (sy - 3 >= 2.5f && sx2 - 0.1f >= ex[1] - 0.5f && sx2 - 0.1f <= ex[1] + 0.5f)
        {
            vida_enemies[1] -= 1;
            sy = 0;
            space = false;
        }
        if (sy - 3 >= 2.5f && sx2 - 0.1f <= ex[2] + 6.5f && sx2 - 0.1f >= ex[2] + 5.5f)
        {
            vida_enemies[2] -= 1;
            sy = 0;
            space = false;
        }
        
    }

    void gameover()
    {
        if (vida <= 0)
        {

        }
    }
}

