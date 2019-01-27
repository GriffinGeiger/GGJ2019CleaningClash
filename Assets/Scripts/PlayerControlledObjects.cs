﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlledObjects : MonoBehaviour
{
    public abstract void Move(Vector2 Velocity);
    public abstract void Aim(Vector2 AimVector);

    public PlayerInput.playerTag m_playerTag;
    [SerializeField]


    public void ConnectToPlayerInput(GameManager gm)
    {
        Debug.Log("connecting");
        if (m_playerTag == PlayerInput.playerTag.None)
        {
            Debug.LogWarning("PlayerTag set to no player. Add playerTag in prefab. Ex: Player1, Player2");
        }
        else
        {
            PlayerInput player = null;
            try
            {
  
                player = gm.getPlayerInput((int)m_playerTag);
            }catch (System.NullReferenceException nre ) { Debug.LogError(nre.Message); }
            
            if (player.m_controller == null)
                {
                    player.m_controller = (PlayerControlledObjects)this;
                }
                else
                {
                    Debug.LogWarning("Replacing PlayerController that is already connected to PlayerInput");
                    player.m_controller = (PlayerControlledObjects)this;
                }
        }
    }
}

