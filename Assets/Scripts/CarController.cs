using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    MyListener _myListener;
    Rigidbody rb;
    [SerializeField] Transform frontLeftWheel, frontRightWheel, backRightWheel, backLeftWheel;
    [SerializeField] bool isAccelerating;
    [SerializeField] bool isBreaking;
    [SerializeField] bool withPotentiometer;

    [SerializeField] float speed;
    [SerializeField] float rotationAngle;
    [SerializeField] float smoothRotationSpeed;
    [SerializeField] float currentDirection;
    [SerializeField] float smoothDirection;
    const int maxDirection = 1023;

    Vector3 dir = Vector3.zero;
    Quaternion _quaternion;

    void Awake()
    {
        _myListener = GetComponent<MyListener>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        speedText.text = " Speed: " + (int)rb.velocity.magnitude;
        Rotation();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Rotation()
    {
        float currentDirection = _myListener.potentiometerRead / maxDirection;
        smoothDirection = Mathf.MoveTowards(smoothDirection, currentDirection, Time.deltaTime * smoothRotationSpeed);

        float lerpDir = Mathf.Lerp(-1, 1, smoothDirection);

        dir = new Vector3(0, lerpDir, 0);
        _quaternion = Quaternion.Euler(dir * rotationAngle);

        frontLeftWheel.rotation = _quaternion;
        frontRightWheel.rotation = _quaternion;
        //rb.MoveRotation(quaternion);
    }
    void Move()
    {
        rb.velocity = frontLeftWheel.forward * _myListener.buttonRead * Time.deltaTime * speed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _quaternion.eulerAngles);
    }
}
