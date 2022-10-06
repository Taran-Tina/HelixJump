using UnityEngine;

public class Controls : MonoBehaviour
{
    public bool iCanMove = false;
    /*
    public float RotationSpeed = 150;

        public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            Level.Rotate(0, -mouseX * RotationSpeed * Time.deltaTime, 0);
        }
    }*/


    // данный способ возвращает объект в позицию мыши, отмен€€ предыдущее действие

    public Transform Level;
    private Vector3 _previousMousePosition;
    public float Sensitivity;

    void Update()
    {
        if (Input.GetMouseButton(0) && iCanMove == true)
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            Level.Rotate(0, -delta.x * Sensitivity, 0);   
        }
        _previousMousePosition = Input.mousePosition;
    }
}
