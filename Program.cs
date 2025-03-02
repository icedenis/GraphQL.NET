using Odyssey.MusicMatcher.Types;
using SpotifyWeb;

var builder = WebApplication.CreateBuilder(args);


//Shopify Service addinng
builder.Services.AddHttpClient<SpotifyService>();

//Addint different type to GraphQl
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterService<SpotifyService>();
//   .AddType <Playlist>();




//Setting Apolo connection to our server
builder
   .Services
   .AddCors(options =>
   {
       options.AddDefaultPolicy(builder =>
       {
           builder
               .WithOrigins("https://studio.apollographql.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
       });
   });


var app = builder.Build();
//Apolo Server Setup
app.UseCors();

app.MapGraphQL();

app.Run();
