using System;
using System.Threading;
using Polimaster.Utils.DimUnits.res;

namespace Polimaster.Utils.DimUnits.channel;

/// <summary>
/// Channel
/// </summary>
public struct Channel {
    /// <summary>
    /// See <see cref="ChannelCode"/>
    /// </summary>
    public ChannelCode Code;

    /// <summary>
    /// Byte value of <see cref="Code"/>
    /// </summary>
    public byte CodeValue => (byte)Code;

    /// <summary>
    /// Returns localized label.
    /// </summary>
    /// <see cref="ToString"/>
    public string Label => ToString();

    /// <summary>
    /// Returns localized label.
    /// </summary>
    public override string ToString() {
        return Resources.ResourceManager.GetString(Enum.GetName(typeof(ChannelCode), Code) ?? string.Empty,
            Thread.CurrentThread.CurrentCulture) ?? string.Empty;
    }
}