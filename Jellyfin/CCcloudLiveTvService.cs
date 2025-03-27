
using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.MediaInfo;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.CCcloud
{
    public class CCcloudLiveTvService : ILiveTvService
    {
        private readonly HttpClient _httpClient;
        private const string DefaultPlaylistUrl = "https://raw.githubusercontent.com/darknessally/firerises/master/plugin.video.ccloudtv/resources/lib/ccloud.m3u";

        public CCcloudLiveTvService()
        {
            _httpClient = new HttpClient();
        }

        public string Name => "CCcloud IPTV";
        public string HomePageUrl => "https://github.com/darknessally/firerises";

        public async Task<IEnumerable<ChannelInfo>> GetChannelsAsync(CancellationToken cancellationToken)
        {
            var channels = new List<ChannelInfo>();
            var m3uUrl = Plugin.Instance.Configuration.CCcloudPlaylistUrl ?? DefaultPlaylistUrl;
            var content = await _httpClient.GetStringAsync(m3uUrl);

            var lines = content.Split('\n');
            ChannelInfo currentChannel = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    var nameStart = line.IndexOf(",") + 1;
                    var name = line.Substring(nameStart).Trim();
                    currentChannel = new ChannelInfo
                    {
                        Id = name,
                        Name = name
                    };

                }
                else if (line.StartsWith("http") && currentChannel != null)
                {
                    channels.Add(currentChannel);


                    channels.Add(currentChannel);
                    currentChannel = null;
                }
            }

            return channels;
        }

        public Task<MediaSourceInfo> GetChannelStream(string id, string mediaSourceId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new MediaSourceInfo
            {
                Id = id,
                Path = id,
                Protocol = MediaProtocol.Http,
                Container = "ts",
                IsRemote = true,
                SupportsDirectPlay = true,
                SupportsDirectStream = true
            });
        }

        public Task<List<MediaSourceInfo>> GetChannelStreamMediaSources(string id, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<MediaSourceInfo>
            {
                new MediaSourceInfo
                {
                    Id = id,
                    Path = id,
                    Protocol = MediaProtocol.Http,
                    Container = "ts",
                    IsRemote = true,
                    SupportsDirectPlay = true,
                    SupportsDirectStream = true
                }
            });
        }

        public Task<IEnumerable<ProgramInfo>> GetProgramsAsync(string channelId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
            => Task.FromResult<IEnumerable<ProgramInfo>>(new List<ProgramInfo>());

        public Task<IEnumerable<TimerInfo>> GetTimersAsync(CancellationToken cancellationToken)
            => Task.FromResult<IEnumerable<TimerInfo>>(new List<TimerInfo>());

        public Task<SeriesTimerInfo> GetNewTimerDefaultsAsync(CancellationToken cancellationToken, ProgramInfo program)
            => Task.FromResult(new SeriesTimerInfo());

        public Task CreateTimerAsync(TimerInfo info, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task UpdateTimerAsync(TimerInfo info, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task CancelTimerAsync(string timerId, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task CreateSeriesTimerAsync(SeriesTimerInfo info, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task UpdateSeriesTimerAsync(SeriesTimerInfo info, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task CancelSeriesTimerAsync(string timerId, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task<IEnumerable<SeriesTimerInfo>> GetSeriesTimersAsync(CancellationToken cancellationToken)
            => Task.FromResult<IEnumerable<SeriesTimerInfo>>(new List<SeriesTimerInfo>());

        public Task CloseLiveStream(string id, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task ResetTuner(string id, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
