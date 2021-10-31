﻿using Sandbox;

namespace ssl.Modules.Elements
{
    [Library("base")]
    public class BaseData : Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }

        public override string ToString()
        {
	        return $"[{this.Id}] {this.Name}";
        }
    }
}