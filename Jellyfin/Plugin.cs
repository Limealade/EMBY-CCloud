
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using System;

namespace Jellyfin.Plugin.CCcloud
{
    public class Plugin : BasePlugin<PluginConfiguration>, IPlugin
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "CCcloud Live TV";
        public override string Description => "Provides CCcloud IPTV streams as Live TV channels in Jellyfin.";
        public static Plugin Instance { get; private set; }
        public override Guid Id => new Guid("a4f218b7-77a9-4ac7-98ea-ccloudjelly");
    }
}
