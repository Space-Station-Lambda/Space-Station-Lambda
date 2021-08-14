﻿using Sandbox;

namespace ssl
{
    /// <summary>
    /// FIXME: Nested network components didn't work. Use this class instead.
    /// </summary>
    public abstract partial class NetworkedEntityAlwaysTransmitted : Entity
    {
        public override void Spawn()
        {
            Transmit = TransmitType.Always;
            base.Spawn();
        }
    }
}