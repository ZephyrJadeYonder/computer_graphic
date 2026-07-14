using i3D;
using System;

public partial class Player : CharacterBulk
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public const float ANGLESPEED = 0.01f;// define the speed of the camera rotation
// the next 3 lines are what I have just wrote.
	private Item3D _body;
	private AnimationPlayer _anim;

	private SpringArm _springarm;

// the full of istart is what I have just wrote.
    public override void iStart()
    {
        SetUpdate(true);
		SetFixedupdate(true);

		_body = GetItem<Item3D>("character-a2");
		_anim = _body.GetItem<AnimationPlayer>("AnimationPlayer");
		_springarm = GetItem<SpringArm>("SpringArm");
    }


	public override void FixedUpdate(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			//_anim.Play("sprint");//add jump animation there
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			_body.LookAt(_body.GlobalPosition + direction, Vector3.Up);// waht I have write.
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			if (IsOnFloor() && velocity.Y == 0) _anim.Play("sprint");//if on the floor and not jump, play run animation
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			if (IsOnFloor() && velocity.Y == 0) _anim.Play("idle");//if on the floor and not jump, play idle animation
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	// the below is what I have just wrote, it is to control the camera rotation with mouse.
	public override void OnInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Right)//if right mouse button is pressed
		{
			// the if statement is to capture the mouse when right mouse button is pressed, and release the mouse when right mouse button is released.
			if (mouseButton.Pressed)
			{
				Input.SetMouseMode(Input.MouseModeEnum.Captured);//capture the mouse
			}
			else
			{
				Input.SetMouseMode(Input.MouseModeEnum.Visible);//release the mouse
			}
		}
		if (@event is InputEventMouseMotion mouseMotion)
		{
			// the if statement is to rotate the camera when right mouse button is pressed and mouse is moved.
			// but I still don't  undestand the logic of the below code, I will try to figure it out later.
			if (Input.IsMouseButtonPressed(MouseButton.Right))
			{
				Vector3 y = Rotation;
				y.Y -= mouseMotion.Relative.X * ANGLESPEED;
				Rotation = y;
				Vector3 x = _springarm.Rotation;
				x.X -= mouseMotion.Relative.Y * ANGLESPEED;
				_springarm.Rotation = x;
			}
		}
	}
}
s