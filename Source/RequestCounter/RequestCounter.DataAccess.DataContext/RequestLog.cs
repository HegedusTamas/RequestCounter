using System.ComponentModel.DataAnnotations;

namespace RequestCounter.DataAccess.DataContext;

public class RequestLog
{
    [Key]
    public int RequestLogId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string EndPointName { get; set; } = null!;

    [Required]
    public int RequestCount { get; set; }
}
