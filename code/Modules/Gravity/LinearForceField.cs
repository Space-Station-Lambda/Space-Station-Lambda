using System.Collections.Generic;
using Sandbox;

namespace ssl.Modules.Gravity;

[Library("ssl_force_linear")]
public class LinearForceField : BaseForceField
{
    [Property] public Vector3 ForceDirection { get; set; }

    protected override Vector3 ForceFromEntity(Entity ent)
    {
        return ForceDirection;
    }
}