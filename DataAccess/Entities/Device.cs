namespace DataAccess.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public int ProducerID { get; set; }

        public Producer Producer { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }
    }
}
