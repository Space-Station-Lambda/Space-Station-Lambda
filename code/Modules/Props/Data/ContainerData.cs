﻿using Sandbox;

namespace ssl.Modules.Props.Data
{
    /// <summary>
    /// A container of specific think. ex: gravel or bucket
    /// </summary>
    [Library("container")]
    public class ContainerData : PropData
    {
        public int Capacity { get; set; }
    }
}