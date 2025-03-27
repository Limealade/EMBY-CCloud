
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.CCcloud
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string CCcloudPlaylistUrl { get; set; } = "https://raw.githubusercontent.com/darknessally/firerises/master/plugin.video.ccloudtv/resources/lib/ccloud.m3u";
    }
}
