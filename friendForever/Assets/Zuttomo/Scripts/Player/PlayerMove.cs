using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{

    public void RunnerMovement(Rigidbody rigidBody, RunnerDoingState runnerDoingState , PlayerInput playerInput, PlayerStatus playerStatus)
    {
        Vector3 move = new Vector3(0f, 0f, 0f);
        move.x = playerInput.axisKey_x;
        move.z = playerInput.axisKey_z;

        switch (runnerDoingState)
        {
            case RunnerDoingState.run:
                playerStatus.speed = playerStatus.runSpeed;
                break;
            case RunnerDoingState.walk:
                playerStatus.speed = playerStatus.walkSpeed;
                break;
        }

        //rigidBody.MovePosition(player.transform.position + move);
        rigidBody.velocity = move * playerStatus.speed;
    }
}
