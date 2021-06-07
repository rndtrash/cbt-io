using Sandbox;

namespace CBTIO
{
	partial class CBTPlayer : Player
	{
		public override void Respawn()
		{
			base.Respawn();
			Rotation = Rotation.FromAxis( Vector3.Zero, 0 );

			SetModel( "models/hamster.vmdl" );

			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new CBTController(); //new WalkController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = null; //new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			Camera = new CBTCamera();

			EnableAllCollisions = false;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			Transmit = TransmitType.Always;
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			//
			// If you have active children (like a weapon etc) you should call this to 
			// simulate those too.
			//
			SimulateActiveChild( cl, ActiveChild );

			//
			// If we're running serverside and Attack1 was just pressed, spawn an agent
			//
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{
				var agent = new BaseAgent();
				agent.SetModel( "models/hamster.vmdl" );
				agent.Position = EyePos + EyeRot.Forward * 40;
				agent.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				agent.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				//agent.Spawn();
			}
		}

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}

		public override void BuildInput( InputBuilder input )
		{
			base.BuildInput( input );

			input.AnalogLook = Angles.Zero;
			input.ViewAngles = Angles.Zero;
		}
	}
}
