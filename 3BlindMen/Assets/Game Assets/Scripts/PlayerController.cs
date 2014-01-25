using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    enum State
    {
        Idle, Rotating, Moving
    };

    State currentState = State.Idle;
    Vector3 rotationalVelocity;

    const int ROTATION_SPEED = 15;
    const float MOVEMENT_SPEED = 0.2f;

    float remainingRotation;
    float remainingMovement;

	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case State.Idle:
                float rotateHorizontal = Input.GetAxis("Horizontal");
                float rotateVertical = Input.GetAxis("Vertical");
                bool moveForward = Input.GetButtonDown("Move");
                if (rotateHorizontal > 0)
                {
                    //currentState = State.RotateRight;
                    rotationalVelocity = new Vector3(0, ROTATION_SPEED, 0);
                    currentState = State.Rotating;
                    remainingRotation = 90;
                }
                else if (rotateHorizontal < 0)
                {
                    //currentState = State.RotateLeft;
                    rotationalVelocity = new Vector3(0, -ROTATION_SPEED, 0);
                    currentState = State.Rotating;
                    remainingRotation = 90;
                }
                else if (rotateVertical > 0)
                {
                    //currentState = State.RotateUp;
                    rotationalVelocity = new Vector3(0, 0, ROTATION_SPEED);
                    currentState = State.Rotating;
                    remainingRotation = 90;
                }
                else if (rotateVertical < 0)
                {
                    //currentState = State.RotateDown;
                    rotationalVelocity = new Vector3(0, 0, -ROTATION_SPEED);
                    currentState = State.Rotating;
                    remainingRotation = 90;
                }
                else if (moveForward)
                {
                    currentState = State.Moving;
                    remainingMovement = 1f;
                }
                break;

            case State.Moving:
                transform.Translate(Vector3.right * MOVEMENT_SPEED, Space.Self);
                remainingMovement -= MOVEMENT_SPEED;
                if (remainingMovement <= 0)
                {
                    transform.position = new Vector3(
                        Mathf.Round(transform.position.x),
                        Mathf.Round(transform.position.y),
                        Mathf.Round(transform.position.z));
                    currentState = State.Idle;
                }
                break;

            case State.Rotating:
                transform.Rotate(rotationalVelocity, Space.Self);
                remainingRotation -= ROTATION_SPEED;
                if (remainingRotation <= 0)
                {
                    currentState = State.Idle;
                }
                break;
        };
	}
}
