using Pylon.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Domain.Entities.Base
{
	public abstract class BaseEntity
	{
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string? CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string? UpdatedBy { get; set; }
		public bool IsActive { get; set; } = true;


		// Custom Properties (Doesnt Exists in DataBase)
		[NotMapped]
		public StateType StateType { get; set; } = StateType.Insert;
		[NotMapped]
		public object? Property1 { get; set; }
		[NotMapped]
		public object? Property2 { get; set; }
		[NotMapped]
		public object? Property3 { get; set; }
	}
}
