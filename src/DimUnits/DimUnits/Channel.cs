using System;
using System.Collections.Generic;
using System.Threading;
using Polimaster.Utils.DimUnits.res;

namespace Polimaster.Utils.DimUnits {
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

    public static class Channels {
        private static List<Channel> _LIST;

        /// <summary>
        /// List of avaliable channels (ChannelCode.NONE channel is not included).
        /// </summary>
        public static List<Channel> LIST {
            get {
                if (_LIST == null) {
                    _LIST = new List<Channel>();
                    foreach (var channel in DICTIONARY) _LIST.Add(channel.Value);
                }

                return _LIST;
            }
        }

        /// <summary>
        /// Dictionary of avaliable channels (ChannelCode.NONE channel is not included).
        /// </summary>
        public static readonly Dictionary<ChannelCode, Channel> DICTIONARY = new Dictionary<ChannelCode, Channel> {
            { ChannelCode.ALPHA, new Channel { Code = ChannelCode.ALPHA } },
            { ChannelCode.BETA, new Channel { Code = ChannelCode.BETA } },
            { ChannelCode.GAMMA, new Channel { Code = ChannelCode.GAMMA } },
            { ChannelCode.NONE, new Channel { Code = ChannelCode.NONE } }
        };
    }
}