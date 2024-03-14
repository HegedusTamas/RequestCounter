using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestCounter.DataAccess.DataContext.Migrations
{
    public partial class AddIncrementCounterStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE IncrementRequestCounter 
                (
                  @EndpointName VARCHAR(MAX)
                )
                AS
                BEGIN
                  UPDATE RequestLogs
                  SET RequestCount = RequestCount + 1
                  WHERE EndpointName = @EndpointName AND Date = CAST(GETDATE() AS DATE);

                  IF @@ROWCOUNT = 0
                  BEGIN
                    INSERT INTO RequestLogs(EndpointName, Date, RequestCount)
                    VALUES (@EndpointName, CAST(GETDATE() AS DATE), 1);
                  END
                END;
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC IncrementRequestCounter;");
        }
    }
}
