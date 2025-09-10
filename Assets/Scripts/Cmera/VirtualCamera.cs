using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
   CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        // Player - 플레이어 생명주기를 완전하게 관리하는 게 아니라면 위험성이 있는 방법
        if(PlayerManager.Instance.Player == null)
            vcam.Follow = PlayerManager.Instance.Player.transform;   
    }
}
