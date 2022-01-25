using System;
using Sandbox;

namespace ssl.Modules;

/// <summary>
///     Classes that implements this interface are representing entities accepting electricity.
///     They can be turned on and off.
/// </summary>
public interface IElectrical
{
    [Input]
    public void TurnOn();
    [Input]
    public void TurnOff();
}