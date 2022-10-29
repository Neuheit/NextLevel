using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform arms;

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

        var rotArms = arms.transform.rotation.eulerAngles;
        var rotPlayer = player.transform.rotation.eulerAngles;

        rotArms.x -= rotAmountY;
        rotArms.z = 0;

        rotPlayer.y += rotAmountX;

        arms.rotation = Quaternion.Euler(rotArms);
        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
