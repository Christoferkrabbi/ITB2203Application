namespace ITB2203Application.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Session SessionId { get; set; }
        public int SeatNo { get; set; }
        public int  Price { get; set; }
    }
}
