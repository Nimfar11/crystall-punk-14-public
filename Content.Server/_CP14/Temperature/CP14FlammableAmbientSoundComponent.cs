using Content.Server.Temperature.Systems;

namespace Content.Server._CP14.Temperature;

/// <summary>
/// CTurn on and turn off AmbientSound when Flammable OnFire os changed
/// </summary>
[RegisterComponent, Access(typeof(EntityHeaterSystem))]
public sealed partial class CP14FlammableAmbientSoundComponent : Component
{
}
