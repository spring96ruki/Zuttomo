﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{

    public void RunnerMovement(Rigidbody rigidBody, RunnerState runnerState , PlayerInput playerInput, PlayerStatus playerStatus)
    {
        Vector3 move = new Vector3(0f, 0f, 0f);
        move.x = playerInput.axisKey_x;
        move.z = playerInput.axisKey_z;

        switch (runnerState)
        {
            case RunnerState.run:
                playerStatus.speed = playerStatus.runSpeed;
                break;
            case RunnerState.walk:
                playerStatus.speed = playerStatus.walkSpeed;
                break;
        }

        //rigidBody.MovePosition(player.transform.position + move);
        rigidBody.velocity = move * playerStatus.speed;
    }
}