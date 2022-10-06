using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsGood ;
    public bool IsBonus ;
    public bool isFinish;
    public bool isBad;

    private void OnCollisionEnter(Collision collision)
    {
        Game gm = GameObject.Find("GameManager").GetComponent<Game>();

        if (collision.collider.TryGetComponent(out Ball ball))
        {
            // исключаем возможность застревани€ на платформе при трении о ее край
            Vector3 normal = -collision.contacts[0].normal.normalized;
            float dot = Vector3.Dot(normal, Vector3.up);
            if (dot >= 0.5)
            {
                // если все услови€ выполн€ютс€, то игрок прыгает
                if (IsGood && ball.breakCount < 3 && gm.CurrentState==Game.State.Playing)
                { 
                    ball.Bounce(); 
                }
                if (IsBonus==true && ball.breakCount < 3 && gm.CurrentState == Game.State.Playing)
                {
                    ball.Bounce();
                    Destroy(gameObject);
                    ball.breakCount = 0;
                    ball.Score(2);
                }
                if (collision.gameObject.name == "Ball" && ball.gotPower== true && isFinish == false)
                {                    
                    Destroy(gameObject);
                    ball.breakCount = 0;
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(0, -ball.BounceSpeed/2, 0);
                    ball.Score(5);
                }
                if (isBad == true)
                {
                    if (ball.gotPower == false)
                    {
                        ball.Die();                     
                    }
                    else
                    {
                        Destroy(gameObject);
                        ball.breakCount = 0;
                        ball.GetComponent<Rigidbody>().velocity = new Vector3(0, -ball.BounceSpeed / 2, 0);
                    }                    
                }
                if (isFinish == true)
                {
                    ball.ReachFinish();
                }
            }


        }
    }
}
