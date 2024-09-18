using FluentMigrator;

namespace PizzaOrderAPI.Data.Migrations
{
    [Migration(34)]
    public class CreateTabels : Migration
    {
        public override void Down()
        {
          
        }

        public override void Up()
        {

            Create.Table("pizzas")
                       .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                       .WithColumn("Name").AsString().NotNullable()
                       .WithColumn("Description").AsString().NotNullable()
                       .WithColumn("Price").AsInt32().NotNullable()
                       .WithColumn("Type").AsString().NotNullable();


        }
        private void CreateServices()
        {

            Insert.IntoTable("pizzas").Row(new { Name = "pizza capricioasa", Description = "asdasd", Price = 50, Type = "mare" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza brociuto", Description = "asdasasdasd", Price = 25, Type = "mica" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza brociuto-efungh", Description = "asdgfdfgsdasd", Price = 75, Type = "familie" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza marghirita", Description = "asdasasdasdd", Price = 40, Type = "medie" });

        }
    }
}
