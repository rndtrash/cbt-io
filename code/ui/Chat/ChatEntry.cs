using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;


namespace CBT
{
	public partial class ChatEntry : Panel
	{
		public Label TimeStamp { get; internal set; }
		public Label NameLabel { get; internal set; }
		public Label Message { get; internal set; }
		//public Image Avatar { get; internal set; }

		public ChatEntry()
		{
			//Avatar = Add.Image();
			TimeStamp = Add.Label( "TimeStamp", "timestamp" );
			NameLabel = Add.Label( "Name", "name" );
			Message = Add.Label( "Message", "message" );
		}

		public override void Tick() 
		{
			base.Tick();
		}
	}
}
