using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private int _objectsInSlice = 16;
    public float radius = 5.0f;
    public GameObject ball;
    public GameObject axis;
    public GameObject[] smallPieces;

    public void StartGame()
    {
        ClearHelix();
        //строим последний этаж
       // MakeSlice(-20, 1f, 2);
        //строим срезы
        for (int i=-4; i<5; i++)
        {
            MakeSlice(i*4, 0.8f, 1);
        }
        //строим первый
        MakeSlice(20, 0.99f, 0);

        //создаем шарик
        MakePlayer();
    }

    void MakeSlice(int elevation, float filling, int type)
    {
        Vector3 center = axis.transform.position + new Vector3(0, elevation-1, 0);
        GameObject slice = new GameObject();
        slice.transform.position = center;
        slice.name = "Slice";
        int fill = (int)Mathf.Floor(_objectsInSlice * filling);
        for (int i = 0; i < fill; i++)
        {
            Vector3 pos = Circle(center, radius, i);
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, center - pos);
            GameObject piece;
            int randomEmpty = Random.Range(0, 100);
            int randPiece = 1;
            if (type == 0)
            {
                randPiece = Random.Range(0, smallPieces.Length - 2);
            }
            else if (type==1)
            {
                randPiece = Random.Range(0, smallPieces.Length-1);
            }
            else if (type == 2)
            {
                randPiece = 3;
            }

            if (randomEmpty>85 && filling<0.99f)
            {
                i=i+2;
            }
            piece = Instantiate(smallPieces[randPiece], pos , rot);
            piece.name = smallPieces[randPiece].name;
            piece.tag = "Piece";
            piece.gameObject.transform.parent = slice.gameObject.transform;
                   
        }
        slice.transform.parent = gameObject.transform.GetChild(0);
        float randomAngle = Random.Range(5, 360);
        slice.transform.eulerAngles = new Vector3(slice.transform.eulerAngles.x, slice.transform.eulerAngles.y + randomAngle, slice.transform.eulerAngles.z);
        slice.AddComponent<BoxCollider>();
        BoxCollider bc = slice.GetComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.center = new Vector3(0, 2f, 0);
        bc.size = new Vector3(7, 3.6f, 7);
    }

    void MakePlayer()
    {
        Vector3 center = axis.transform.position + new Vector3(0, 26, 0);
        Vector3 pos = Circle(center, radius, 10) + new Vector3(0, 0, -1.8f);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject player;
        player = Instantiate(ball, pos, rot);
        player.name = "Ball";
    }
    Vector3 Circle(Vector3 center, float radius, int step)
    {
        float ang = 360 / 0.0001f+((360/ _objectsInSlice) *step);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public void ClearHelix()
    {
        int cslices = GameObject.Find("CylinderRoot").transform.childCount;
        for (int i = 0; i < cslices; i++)
        {
            Transform goTr = GameObject.Find("CylinderRoot").transform.GetChild(i);
            if (goTr!= null && goTr.name=="Slice")
            {
                Destroy(goTr.gameObject);
            }
        }

        GameObject player = GameObject.Find("Ball");
        if (player!=null)
        {
            Destroy(player);
        }
    }
}
