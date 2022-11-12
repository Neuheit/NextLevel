using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float mouseSens;

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RotateCamera();
    }

    void RotateCamera()
    {
        var floatX = Input.GetAxis("Mouse X");
        var floatY = Input.GetAxis("Mouse Y");

        var rotAmountX = floatX * mouseSens;
        var rotAmountY = floatY * mouseSens;

        var rotPlayer = player.transform.rotation.eulerAngles;

        rotPlayer.y += rotAmountX;
        rotPlayer.x -= rotAmountY;
        rotPlayer.z = 0;

        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
