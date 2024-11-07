using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Player
    public static string XPlayerMovement;
    public static string YPlayerMovement;
    public static string JumpInput;
    public static string DashInput;
    public static string MeleeAttackInput;
    public static string InteractInput;
    public static string Item1Input;
    public static string Item2Input;
    public static string PauseInput;
    
    //Menu Navigation
    public static string XMenuNavigation;
    public static string YMenuNavigation;
    public static string Confirm;
    

    private void Awake()
    {
        PlayerEnable();
        MenuDisable();
    }
    public static void PlayerEnable()
    {
        XPlayerMovement = "Horizontal";
        YPlayerMovement = "Vertical";
        JumpInput = "Jump";
        DashInput = "Dash";
        MeleeAttackInput = "Melee Attack";
        InteractInput = "Interact";
        Item1Input = "Item 1";
        Item2Input = "Item 2";
        PauseInput = "Cancel";
    }

    public static void PlayerDisable()
    {
        XPlayerMovement = "Empty";
        YPlayerMovement = "Empty";
        JumpInput = "Empty";
        DashInput = "Empty";
        MeleeAttackInput = "Empty";
        InteractInput = "Empty";
        Item1Input = "Empty";
        Item2Input = "Empty";
        PauseInput = "Empty";
    }

    public static void MenuEnable()
    {
        XMenuNavigation = "Horizontal";
        YMenuNavigation = "Vertical";
        Confirm = "Confirm";
    }

    public static void MenuDisable()
    {
        XMenuNavigation = "Empty";
        YMenuNavigation = "Empty";
        Confirm = "Empty";
    }

    public static void EndingEnable()
    {
        InteractInput = "Interact";
    }
}
