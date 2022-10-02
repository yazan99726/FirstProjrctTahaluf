namespace FirstProjectMarketing.Models
{
    public class FirstReviewProduct
    {
        public decimal Id { get; set; }
        public string Review { get; set; }
        public decimal Rating { get; set; }
        public decimal? User_Id { get; set; }
        public decimal? ProductId { get; set; }

        public virtual Firstuser User { get; set; }
        public virtual Firstproduct Product { get; set; }
    }
}
