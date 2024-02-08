namespace TestDrivenHotel.DataAccess
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public int SquareMeters { get; set; }
        public int GuestCapacity { get; set; }
        public string View { get; set; }
    }
}