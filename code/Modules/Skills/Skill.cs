using System;
using System.Collections.Generic;

namespace ssl.Modules.Skills
{
    /// <summary>
    /// A specific skill value. We don't use the experience system for this because wed don't really want to exp.
    /// </summary>
    public abstract class Skill 
    {
        public static Dictionary<string, Skill> All = new(){
            { "strength", new Strength() },
            { "cooking", new Cooking() },
            { "engineering", new Engineering() },
        };
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
    }
}