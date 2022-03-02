namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modfy : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
          
            Sql("ALTER TABLE MembershipTypes DROP COLUMN MembershipTypeId");

        }
        
    }
}
