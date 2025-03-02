using SpotifyWeb;

namespace Odyssey.MusicMatcher.Types
{
    [GraphQLDescription("A curated collection of tracks designed for a specific activity or mood.")]
    public class Playlist
    {

        [GraphQLDescription("The ID for the playlist.")]
        [ID]
       public string Id { get;}
        [GraphQLDescription("The name of the playlist.")]
        public string Name {  get; set; }

        [GraphQLDescription("Describes the playlist, what to expect and entices the user to listen.")]
        public string? Description { get; set; }

        //add another Type to this list like reference
        [GraphQLDescription("List of Tracks")]
        private List<Track> _tracks { get; set; }


        public async Task<List<Track>> Tracks(SpotifyService spotifyService) 
        {
            if (_tracks != null)
            {
                return _tracks;
            }
            else
            {
                var response = await spotifyService.GetPlaylistsTracksAsync(this.Id);
                return response.Items.Select(item => new Track(item.Track)).ToList();
            }
        }
        public Playlist(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Playlist(PlaylistSimplified obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
           
        }

        public Playlist(SpotifyWeb.Playlist obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
            var paginatedTracks = obj.Tracks.Items;
            var trackObjects = paginatedTracks.Select(item => new Track(item.Track));
            this._tracks = trackObjects.ToList();
            //one liner
            // Tracks = obj.Tracks.Items.Select(item => new Track(item.Track)).ToList();4


        }
    }
}
