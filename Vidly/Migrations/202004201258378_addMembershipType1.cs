namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMembershipType1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "membershipType_Id", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "membershipType_Id" });
            RenameColumn(table: "dbo.Customers", name: "membershipType_Id", newName: "MembershipTypeId");
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Customers", "MembershipTypeId");
            AddForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Byte());
            RenameColumn(table: "dbo.Customers", name: "MembershipTypeId", newName: "membershipType_Id");
            CreateIndex("dbo.Customers", "membershipType_Id");
            AddForeignKey("dbo.Customers", "membershipType_Id", "dbo.MembershipTypes", "Id");
        }
    }
}
