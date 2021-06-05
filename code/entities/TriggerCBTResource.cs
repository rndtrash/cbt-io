using Sandbox;
using Sandbox.Hammer;

namespace CBTIO
{
	// TODO: move it somewhere else
	public enum ResourceType
	{
		Oil
	}

	[Library( "trigger_cbt_resource" )]
	public partial class TriggerCBTResource : BaseTrigger
	{
		[Net, HammerProp] public int Units { get; set; }
		[HammerProp] public ResourceType Type { get; internal set; }

		public override void Spawn()
		{
			base.Spawn();
		}

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );
		}
	}
}
