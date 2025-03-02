using SpotifyWeb;
using System.Security.Cryptography.X509Certificates;

namespace Odyssey.MusicMatcher.Types
{
    public class Query
    {
        //[GraphQLDescription("Playlists hand-picked to be Branipolsipicks")]
        //public List<Playlist> FeaturePlaylists()
        //{
        //    return new List<Playlist>
        //    {
        //       new Playlist("1", "GraphQL Sezi'"),
        //       new Playlist("2", "Graph Explorer Denis"),
        //       new Playlist("3", "Interpretive GraphQL Branipole")
        //    };
          
        //}

        public  async Task<List<Playlist>> FeaturedPlaylists([Service] SpotifyService spotifyService)
        {
            var response = await spotifyService.GetFeaturedPlaylistsAsync();

            var items = response.Playlists.Items;

            var playlists = items.Select(item => new Playlist(item));
            //return response.Playlists.Items.Select(item => new Playlist(item)).ToList();
            return playlists.ToList();
        }

        [GraphQLDescription("Retrieves a specific playlist.")]
        public async Task<Playlist?> GetPlaylist([ID] string id,SpotifyService spotifyService)
        {
            var response = await spotifyService.GetPlaylistAsync(id);
            var playlist = new Playlist(response);
            return playlist;
        }
       
    }
}
