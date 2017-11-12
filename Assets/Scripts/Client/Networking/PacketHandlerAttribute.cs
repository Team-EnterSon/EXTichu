using EXTichu.Common;
using System;

namespace EXTichu.Client
{
	[AttributeUsage(AttributeTargets.Event)]
	public sealed class PacketHandler : Attribute
	{
		public Type PacketType { get; private set; }

		private PacketHandler() { }
		public PacketHandler(Type packetType)
		{
			this.PacketType = packetType;
		}
	}
}
