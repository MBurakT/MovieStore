namespace WebApi.Entities;

public class CustomerGenre
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public CustomerGenre(int customerId, int genreId)
    {
        CustomerId = customerId;
        GenreId = genreId;
    }

    public CustomerGenre(Customer customer, Genre genre)
    {
        Customer = customer;
        Genre = genre;
    }
}