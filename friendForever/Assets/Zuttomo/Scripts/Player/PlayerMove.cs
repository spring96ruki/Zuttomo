using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    float coefficient = 0.01f;

    public void RunnerMovement(GameObject player, Rigidbody rigidBody, RunnerDoingState runnerDoingState , PlayerInput playerInput, PlayerStatus playerStatus)
    {
        Vector3 move = new Vector3(0f, 0f, 0f);
        move.x = playerInput.axisKey_x;
        move.z = playerInput.axisKey_z;

        switch (runnerDoingState)
        {
            case RunnerDoingState.run:
                playerStatus.speed = playerStatus.runSpeed * coefficient;
                break;
            case RunnerDoingState.walk:
                playerStatus.speed = playerStatus.walkSpeed * coefficient;
                break;
        }

        rigidBody.MovePosition(player.transform.position + move * playerStatus.speed);
    }

    public void ChaserMovement(GameObject player, Rigidbody rigidBody, PlayerInput playerinput, PlayerStatus playerStatus)
    {
        Vector3 move = new Vector3(0f, 0f, 0f);
        move.x = playerinput.axisKey_x;
        move.z = playerinput.axisKey_z;

        playerStatus.speed = playerStatus.runSpeed * coefficient;
        //rigidBody.velocity = move * playerStatus.speed;
        rigidBody.MovePosition(player.transform.position + move * playerStatus.speed);
    }
}
