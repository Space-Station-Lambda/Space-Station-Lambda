﻿using System.Data;

namespace Sandbox
{
    /// <summary>
    /// Utilisé dans https://github.com/Facepunch/sbox-hidden/blob/739f702340ee7f9f142ced7e93ab609feaa354eb/code/player/Player.Ammo.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface NetList<in T>
    {
        void Clear();
        void Get(int i); // Une adresse ? 
        void Set(int i, T value);
    }
}