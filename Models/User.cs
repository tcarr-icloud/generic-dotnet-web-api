using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace webapi
{
    public class User
    {
        public long ID { get; set; }
        [Required] public Guid Guid { get; set; }
        [Required] public bool Deleted { get; set; }
        [Required] public bool EmailConfirmed { get; set; }
        [Required] public bool PhoneNumberConfirmed { get; set; }
        [Required] public bool TwoFactorEnabled { get; set; }
        [Required] public bool LockoutEnabled { get; set; }
        [Required] public int AccessFailedCount { get; set; }
        [Required] public string Username { get; set; }
        [Required] public bool PasswordReset { get; set; }
        [Required] public DateTime PasswordResetDate { get; set; }
        [Required] public bool AccountDisabled { get; set; }
        [Required] public DateTime TS { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        User ModifiedBy { get; set; }

        public Company? Company { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime? ResetTokenExpire { get; set; }
        public string? BadgeNumber { get; set; }
        public string? Email { get; set; }
        public string? EmailUpdate { get; set; }
        public string? EmployeeDepartment { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeRole { get; set; }
        public string? FCMTokens { get; set; }
        public string? FirstName { get; set; }
        public string? InternalEmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Pin { get; set; }
        public string? ProfileImg { get; set; }
        public string? ResetToken { get; set; }
        public string? SecurityStamp { get; set; }
        public string? Theme { get; set; }
        public Workspace? Workspace { get; set; }
    }
}