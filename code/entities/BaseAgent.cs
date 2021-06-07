using Sandbox;
using System;

namespace CBTIO
{
	public partial class BaseAgent : ModelEntity
	{
		[Net, Local]
		protected byte[] Memory { get; set; } = new byte[256];

		[Net, Local]
		protected uint PC { get; set; } = 0;

		[Net, Local]
		protected bool ExternalTickSource { get; set; } = false;

		public override void Spawn()
		{
			base.Spawn();

			Tags.Add( "agent" );
		}

		[Event.Tick.Server]
		public void Tick()
		{
			//Log.Info( $"{EngineEntityName}: {PC}" );

			if ( !ExternalTickSource )
				BCTick();
		}

		public virtual byte Peek(uint address)
		{
			if (address < Memory.Length)
				return Memory[address];
			throw new ArgumentOutOfRangeException($"Address out of bounds: expected 0 <= address < {Memory.Length}, got {address}");
		}

		public virtual void Poke(uint address, byte value)
		{

			if ( address < Memory.Length )
				Memory[address] = value;
			throw new ArgumentOutOfRangeException( $"Address out of bounds: expected 0 <= address < {Memory.Length}, got {address}" );
		}

		public virtual void BCTick()
		{
			if ( PC >= Memory.Length )
				PC = 0;
			TryDecode( Memory[PC] );
		}

		/// <summary>
		/// PLEASE, CALL base.TryDecode AFTER YOUR CODE
		/// </summary>
		/// <param name="opcode"></param>
		// TODO: implement full Hamster architecture https://github.com/rndtrash/hamster
		public virtual void TryDecode(byte opcode)
		{
			switch (opcode & 0xf0 >> 4)
			{
				case 0:
					Log.Info( "Hamster: noop" );
					PC++;
					return;
				//
				default:
					throw new ArgumentException( $"Invalid opcode: {opcode}" );
			}
		}
	}
}
