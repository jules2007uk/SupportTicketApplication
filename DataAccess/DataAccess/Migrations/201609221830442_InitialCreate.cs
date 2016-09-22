namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Owner = c.String(),
                        Assignee = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ticket", t => t.Ticket_ID)
                .Index(t => t.Ticket_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "Ticket_ID", "dbo.Ticket");
            DropIndex("dbo.Comment", new[] { "Ticket_ID" });
            DropTable("dbo.Comment");
            DropTable("dbo.Ticket");
        }
    }
}
