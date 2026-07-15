using i3D;
using System;

public partial class Platform : MeshRender
{
    [Export] public Vector3 Apos = Vector3.Zero;
    [Export] public Vector3 Bpos = Vector3.Zero;

    public void UpdatePosition(int status)
    {
        Tween tween = CreateTween();
        if (status == 0)
        {
            tween.TweenProperty(this, "position", Apos, 3.0f);
        }
        else
        {
            tween.TweenProperty(this, "position", Bpos, 3.0f);
        }

    }
}