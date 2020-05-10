using FluentMigrator;

namespace Din.Infrastructure.Migrations
{
    [Migration(20200506)]
    public class UpdateAddedContent : Migration
    {
        public override void Up()
        {
            Alter.Table("added_content")
                .AlterColumn("system_id").AsInt32().NotNullable()
                .AlterColumn("foreign_id").AsInt32().NotNullable();

            Delete
                .Column("eta")
                .Column("percentage")
                .FromTable("added_content").InSchema("");
        }

        public override void Down()
        {
            Alter.Table("added_content")
                .AlterColumn("system_id").AsString().NotNullable()
                .AlterColumn("foreign_id").AsString().NotNullable();

            Create.Column("eta").OnTable("added_content").AsInt32().Nullable();

            Create.Column("percentage").OnTable("added_content").AsInt32().Nullable();
        }
    }
}
