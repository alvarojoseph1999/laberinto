using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovimiento : MonoBehaviour
{
    [SerializeField]
    private Transform objASeguir;
    [SerializeField]
    private float velCamara = 120;
    [SerializeField]
    private float sensibilidad = 150;

    private float mouseX;
    private float mouseY;
    private float rotY = 0;
    private float rotX = 0;
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * sensibilidad * Time.deltaTime;
        rotX -= mouseY * sensibilidad * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -60, 60);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objASeguir.position, velCamara * Time.deltaTime);
    }
}
