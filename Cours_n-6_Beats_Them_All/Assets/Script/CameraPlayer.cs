using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float xClampLeft;
    [SerializeField] float xClampRight;

    Vector3 dirPlayerToCam;

    private void Start()
    {
        dirPlayerToCam = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Follow();
        //MoveToPlayer();
        FollowClamp();
    }

    private void Follow()
    {
        Vector3 posCam = player.transform.position;
        posCam += dirPlayerToCam;


        transform.position = posCam;
    }


    private void MoveToPlayer()
    {
        Vector3 posCam = player.transform.position;
        posCam += dirPlayerToCam;


        Vector3 clampPosition = Vector3.Lerp(transform.position, posCam, Time.deltaTime);
        clampPosition.x = Mathf.Clamp(clampPosition.x, 1.4f, 48.6f);

        transform.position = clampPosition;
    }

    private void FollowClamp()
    {
        if (isOutOfBoundaries())
        {
            MoveToPlayer();
        }

    }

    bool isOutOfBoundaries()
    {
        //R�cup�rer la position X du player sur l'�cran
        float screenPosPlayer = Camera.main.WorldToScreenPoint(player.transform.position).x;

        //On impose des limites au placement du player sur l'�cran, et si elles sont d�pass�e la cam�ra se met � suivre le joueur
        if (screenPosPlayer > xClampLeft && screenPosPlayer < Screen.width - xClampRight)
        {
            return false;
        }

        return true;

    }

}
