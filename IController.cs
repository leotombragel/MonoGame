using System.Numerics;

public interface IController
{
    void Update();
    //might have: 
    //bool IsBackPressed();
    //bool IsSelectePressed();
    void HandleInput();
    //might have: 
    //    void HandleKeys();
    //    void HandleButtons();
}

//Vector2 GetMovementInput();

