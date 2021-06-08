using Sandbox;
using System;

namespace CBTIO
{
	public partial class BaseAgent : ModelEntity
	{
		[Net, Local]
		protected byte[] AddressSpace { get; set; } = new byte[0xffff];

		protected uint MemorySize = 0x1000; // 0 <= addr < MemorySize

		[Net, Local]
		protected int[] Registers { get; set; } = new int[10];

		protected readonly uint[] InterruptTable = new uint[8]
			{
				0xFF10,
				0xFF12,
				0xFF14,
				0xFF16,
				0xFF18,
				0xFF1A,
				0xFF1C,
				0xFF1E
			};


		[Net, Local]
		protected uint PC { get; set; } = 0;

		[Net, Local]
		protected uint SP { get; set; } = 0xFE00;

		[Net, Local]
		protected uint StackSize { get; set; } = 0xff;

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
			if (address < AddressSpace.Length)
				return AddressSpace[address];
			throw new ArgumentOutOfRangeException($"Address out of bounds: expected 0 <= address < {AddressSpace.Length}, got {address}");
		}

		public virtual void Poke(uint address, byte value)
		{

			if ( address < AddressSpace.Length )
				AddressSpace[address] = value;
			throw new ArgumentOutOfRangeException( $"Address out of bounds: expected 0 <= address < {AddressSpace.Length}, got {address}" );
		}

		public virtual void BCTick()
		{
			if ( PC >= AddressSpace.Length )
				PC = 0;
			TryDecode( AddressSpace[PC] );
		}

		/// <summary>
		/// PLEASE, CALL base.TryDecode AFTER YOUR CODE
		/// </summary>
		/// <param name="opcode"></param>
		// TODO: implement full Hamster architecture https://github.com/rndtrash/hamster
		public virtual void TryDecode(byte opcode)
		{
			byte category = (byte)(opcode & 0xf0 >> 4);
			byte instruction = (byte)(opcode & 0x0f);

			switch (category)
			{
				case 0:
					HamVM_General(instruction);
					return;
				//
				default:
					throw new ArgumentException( $"Invalid opcode: {opcode}" );
			}
		}

		protected void HamVM_General(byte instruction)
		{
			switch (instruction)
			{
				case 0:
					Log.Info( "Hamster: noop" );
					PC++;
					return;
				case 1:
					Log.Error( "TODO: halt and catch fire" );
					PC++;
					return;
				case 2:
					Log.Error( "TODO: jmp to interrupt from it and save prev pos" );
					PC++;
					return;
				default:
					throw new ArgumentException( $"Invalid general instruction: {instruction}" );
			}
		}
	}
}
