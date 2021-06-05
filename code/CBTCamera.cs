using Sandbox;

namespace CBTIO
{
	public class CBTCamera : Camera
	{
		Vector3 TargetPos;
		Rotation TargetRot;

		public CBTCamera()
		{
			var player = Local.Client?.Pawn;
			if ( player != null )
			{
				Pos = player.Position;
				TargetPos = Pos;

				Rot = player.Rotation;
				TargetRot = Rot;
			}
		}

		public override void Update()
		{
			FieldOfView = 70;
			Pos = TargetPos;
			Rot = TargetRot;
			Viewer = null;
		}

		public override void BuildInput( InputBuilder input )
		{
			base.BuildInput( input );

			input.ViewAngles = new Angles(0.0f, 90.0f, 0.0f);
			input.StopProcessing = true;
		}
	}
}
