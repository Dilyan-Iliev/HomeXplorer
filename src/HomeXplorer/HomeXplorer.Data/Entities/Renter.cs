namespace HomeXplorer.Data.Entities
{
    public class Renter
    {
        public Renter()
        {
            this.RentedProperties = new HashSet<Property>();
            this.FavouriteProperties = new HashSet<Property>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string PhoneNumber { get; set; }

        //public string Email { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Property> RentedProperties { get; set; }

        public virtual ICollection<Property> FavouriteProperties { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
