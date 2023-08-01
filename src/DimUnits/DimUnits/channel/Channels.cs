using System.Collections.Generic;

namespace Polimaster.Utils.DimUnits.channel;

/// <summary>
/// Available channels
/// </summary>
public static class Channels {
    private static List<Channel>? _LIST;

    /// <summary>
    /// List of available channels (ChannelCode.NONE channel is not included).
    /// </summary>
    public static List<Channel> List {
        get {
            if (_LIST != null) return _LIST;
            _LIST = new List<Channel>();
            foreach (var channel in DICTIONARY) _LIST.Add(channel.Value);

            return _LIST;
        }
    }

    /// <summary>
    /// Dictionary of available channels (ChannelCode.NONE channel is not included).
    /// </summary>
    public static readonly Dictionary<ChannelCode, Channel> DICTIONARY = new() {
        { ChannelCode.ALPHA, new Channel { Code = ChannelCode.ALPHA } },
        { ChannelCode.BETA, new Channel { Code = ChannelCode.BETA } },
        { ChannelCode.GAMMA, new Channel { Code = ChannelCode.GAMMA } },
        { ChannelCode.NONE, new Channel { Code = ChannelCode.NONE } }
    };
}