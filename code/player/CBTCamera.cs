using Sandbox;

namespace CBTIO
{
	public class CBTCamera : Camera
	{
		Vector3 Elevation = new Vector3( 0, 0, 50.0f );
		Vector3 ActualElevation;
		public CBTCamera()
		{
			ActualElevation = Elevation;

			var player = Local.Client?.Pawn;
			if ( player != null )
			{
				Pos = player.Position;
				Rot = Rotation.FromPitch( 90.0f );
			}
			FieldOfView = 80;
			Viewer = null;
		}

		public override void Update()
		{
			var player = Local.Client?.Pawn;
			if ( player == null )
				return;

			Elevation.z = MathX.Clamp( Elevation.z - Local.Client.Input.MouseWheel * 25.0f, 100.0f, 1000.0f );
			ActualElevation.z = MathX.LerpTo( ActualElevation.z, Elevation.z, 15f * Time.Delta );

			Pos = player.Position + ActualElevation;
		}

		public override void BuildInput( InputBuilder input )
		{
			base.BuildInput( input );
		}
	}
}
