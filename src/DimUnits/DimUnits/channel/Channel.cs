using System;
using System.Threading;
using Polimaster.Utils.DimUnits.res;

namespace Polimaster.Utils.DimUnits.channel; 

public struct Channel {
    public ChannelCode Code;
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
        return Resources.ResourceManager.GetString(Enum.GetName(typeof(ChannelCode), Code),
            Thread.CurrentThread.CurrentCulture);
    }
}