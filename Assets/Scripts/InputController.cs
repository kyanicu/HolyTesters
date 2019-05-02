using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    //enum type used to keep track of hwo the input from user should be handled
    private enum InputState { MENU, GAMEPLAY, PAUSE }
    private InputState currentState;

    //external scene/script references
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //initialize state
        currentState = InputState.GAMEPLAY;

        //initialize external references
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    private void RunGameplayFrameInput()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {

            Vector2 moveDirection = Vector2.zero;


            if (Input.GetKey(KeyCode.A))
                moveDirection += Vector2.left;
            if (Input.GetKey(KeyCode.D))
                moveDirection += Vector2.right;
            if (Input.GetKey(KeyCode.W))
                moveDirection += Vector2.up;
            if (Input.GetKey(KeyCode.S))
                moveDirection += Vector2.down;
    
            moveDirection.Normalize();
            playerController.InitiateDash(moveDirection);
        
        }
    }

    private void RunGameplayFixedInput()
    {
        if (!Input.anyKey)
            return;

        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector2.right;
        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector2.down;

        playerController.MovePlayer(moveDirection);

    }

    private void RunMenuInput()
    {

    }

    private void RunPauseInput()
    {

    }

    private void FixedUpdate()
    {
        if (currentState == InputState.GAMEPLAY)
            RunGameplayFixedInput();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case (InputState.MENU):
                RunMenuInput();
                break;
            case (InputState.GAMEPLAY):
                RunGameplayFrameInput();
                break;
            case (InputState.PAUSE):
                RunPauseInput();
                break;
            default:
                break;
        }

    }
}
