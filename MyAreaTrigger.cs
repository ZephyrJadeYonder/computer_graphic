using i3D;
using System;

public partial class MyAreaTrigger : AreaTrigger
{
    [Export] public Platform movePlatform;
    public override void iStart()
    {
        BodyEntered += OnbodyEntered;
        BodyExited += OnbodyExited;
    }

    public void OnbodyEntered(Item body)
    {
        if (body.Name == "CharacterBulk")
        {
            movePlatform.UpdatePosition(1);
        }
    }
    public void OnbodyExited(Item body)
    {
        if (body.Name == "CharacterBulk")
        {
            movePlatform.UpdatePosition(0);
        }
    }

}
