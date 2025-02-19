using Godot;
using System;

public partial class FPSCamera : Camera3D
{
	[Export] FpsCharacter player;
	[Export] float cameraSpeed = 2f;
	[Export] Vector2 cameraXBound = new Vector2(.75f, .75f);
	[Export] float maxCameraMovementPerFrame = 15f;
	
	float frameDelta;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		frameDelta = (float)delta;
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion mouseMovement)
		{
			Vector2 velocity = mouseMovement.Relative;
			Vector3 rotation = Rotation;

			velocity = new Vector2(
				Mathf.Clamp(velocity.X, -maxCameraMovementPerFrame, maxCameraMovementPerFrame),
				Mathf.Clamp(velocity.Y, -maxCameraMovementPerFrame, maxCameraMovementPerFrame)
			);

			rotation.X += -velocity.Y * cameraSpeed * frameDelta;

			rotation.X = Mathf.Clamp(rotation.X, cameraXBound.Y, cameraXBound.X);
			
			Rotation = rotation;

			Vector3 playerRot = player.Rotation;
			
			playerRot.Y += -velocity.X * cameraSpeed * frameDelta;

			player.Rotation = playerRot;
		}
	}
}
