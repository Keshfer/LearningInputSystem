using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInput : MonoBehaviour
{
    private PlayerInputAction PlayerInputAction;
    private PlayerInput playerInput;
    private Vector2 recordedMovement;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        loadPlayerBinds();

        /*PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Player.Enable();
        PlayerInputAction.Player.Jump.performed += jump;
        PlayerInputAction.UI.Submit.performed += submit;*/

        

        
        
    }
    
    private void Update()
    {
        //print(PlayerInputAction.Player.Movement.ReadValue<Vector2>());
        print(recordedMovement);

        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            playerInput.currentActionMap.Disable();
            playerInput.SwitchCurrentActionMap("UI");
            playerInput.currentActionMap.Enable();
            



        }
        if(Keyboard.current.yKey.wasPressedThisFrame)
        {
            playerInput.currentActionMap.Disable();
            playerInput.SwitchCurrentActionMap("Player");
            playerInput.currentActionMap.Enable();
            
        }
        if(Keyboard.current.mKey.wasPressedThisFrame)
        {
            print("select rebind");
            playerInput.currentActionMap.Disable();
            playerInput.actions["Jump"].PerformInteractiveRebinding()
                .OnComplete(callback => {
                    print(callback);
                    callback.Dispose();
                    playerInput.currentActionMap.Enable();
                    savePlayerBinds();

                })
                .Start();
        }
    }
    public void savePlayerBinds()
    {
        var rebinds = playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        print("saved the bind!");
    }
    public void loadPlayerBinds()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        print("Controls loaded!");
        print(playerInput.actions["Jump"].GetBindingDisplayString());
        


    }

    

    public void jump(InputAction.CallbackContext context)
    {
        //print(context);
        if(context.performed)
        {
            if(context.ReadValue<float>() >0 )
            {
                print("I jump " + context.phase);
            }
            
        }

    }
    public void submit(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            print("Submitted!");
        }
    }
    public void movement(InputAction.CallbackContext context)
    {
        recordedMovement = context.ReadValue<Vector2>();
    }
}
