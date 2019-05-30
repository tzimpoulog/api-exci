namespace ExciApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class POI : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POI",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 250),
                    Description = c.String(maxLength: 500),
                    Longitude = c.Double(nullable: false),
                    Latitude = c.Double(nullable: false),
                    Address = c.String(maxLength: 500),
                    Country = c.String(maxLength: 2),
                    ImageUrl = c.String(maxLength: 250),
                    InfoUrl = c.String(maxLength: 250),
                    PriceList = c.String(maxLength: 250),
                    OpenHours = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.POI");
        }
    }
}
