namespace MlVitrine.Models
{
    public class MeliSession
    {
        public int MeliSessionId { get; set; }

        public long SessionId { get; set; }

        public string? CodeMeli { get; set; }

        public string? Username { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
