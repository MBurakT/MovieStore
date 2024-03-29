namespace WebApi.Entities;

public class MovieActor
{
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public int ActorId { get; set; }
    public Actor? Actor { get; set; }

    public MovieActor(int movieId, int actorId)
    {
        MovieId = movieId;
        ActorId = actorId;
    }

    public MovieActor(Movie movie, Actor actor)
    {
        Movie = movie;
        Actor = actor;
    }
}