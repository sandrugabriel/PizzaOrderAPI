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

            Create.Table("orderdetails")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("OrderId").AsInt32().NotNullable()
                .WithColumn("PizzaId").AsInt32().NotNullable()
                .WithColumn("Price").AsDouble().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable();

            Create.Table("orders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CustomerId").AsInt32().NotNullable()
                .WithColumn("Ammount").AsDouble().NotNullable()
                .WithColumn("Status").AsString().NotNullable();


            Create.Table("Customers")
          .WithColumn("Id").AsInt32().PrimaryKey().Identity()
          .WithColumn("UserName").AsString(256).Nullable()
          .WithColumn("NormalizedUserName").AsString(256).Nullable()
          .WithColumn("Email").AsString(256).Nullable()
          .WithColumn("NormalizedEmail").AsString(256).Nullable()
          .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
          .WithColumn("PasswordHash").AsString().Nullable()
          .WithColumn("SecurityStamp").AsString().Nullable()
          .WithColumn("ConcurrencyStamp").AsString().Nullable()
          .WithColumn("PhoneNumber").AsString().Nullable()
          .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
          .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
          .WithColumn("LockoutEnd").AsDateTime().Nullable()
          .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
          .WithColumn("AccessFailedCount").AsInt32().NotNullable()
          .WithColumn("Name").AsString().NotNullable()
          .WithColumn("Discriminator").AsString().NotNullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString().Nullable()
                .WithColumn("ConcurrencyStamp").AsInt32().Nullable();

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("FK_AspNetUserRoles_Customers", "Customers", "Id")
                .ForeignKey("FK_AspNetUserRoles_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetRolesClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("RoleId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString(256).Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetUserClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("UserId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString().Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_Customers", "Customers", "Id");

            Create.Table("AspNetUserLogins")
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("ProviderKey").AsString(256).PrimaryKey()
           .WithColumn("ProviderDisplayName").AsString().Nullable()
           .WithColumn("UserId").AsInt32().NotNullable()
           .ForeignKey("FK_AspNetUserLogins_Customers", "Customers", "Id");


            Create.Table("AspNetUserTokens")
           .WithColumn("UserId").AsInt32().PrimaryKey()
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("Name").AsString(256).PrimaryKey()
           .WithColumn("Value").AsInt32().Nullable()
           .ForeignKey("FK_AspNetUserTokens_Customers", "Customers", "Id");

            CreatePizzas();
        }

        private void CreatePizzas()
        {

            Insert.IntoTable("pizzas").Row(new { Name = "pizza capricioasa", Description = "asdasd", Price = 50, Type = "mare" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza brociuto", Description = "asdasasdasd", Price = 25, Type = "mica" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza brociuto-efungh", Description = "asdgfdfgsdasd", Price = 75, Type = "familie" });
            Insert.IntoTable("pizzas").Row(new { Name = "pizza marghirita", Description = "asdasasdasdd", Price = 40, Type = "medie" });

        }
    }
}
