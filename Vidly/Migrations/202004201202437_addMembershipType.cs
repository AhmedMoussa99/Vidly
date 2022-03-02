namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        signUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                        MembershipTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "membershipType_Id", c => c.Byte());
            CreateIndex("dbo.Customers", "membershipType_Id");
            AddForeignKey("dbo.Customers", "membershipType_Id", "dbo.MembershipTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "membershipType_Id", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "membershipType_Id" });
            DropColumn("dbo.Customers", "membershipType_Id");
            DropTable("dbo.MembershipTypes");
        }
    }
}
