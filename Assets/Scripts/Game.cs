using UnityEngine;

public class Game : MonoBehaviour
{
    public Controls Controls;
    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    public State CurrentState { get; set; }

    public void OnBallDied()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Loss;
        Debug.Log("Game Over!");
    }

    public void OnBallReachedFinish()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Won;
        Debug.Log("You won!");
        //GameObject.Find("WinSalute").GetComponent<ParticleSystem>().Play();
    }
}
