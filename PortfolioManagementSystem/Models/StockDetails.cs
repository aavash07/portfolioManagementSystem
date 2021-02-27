using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManagementSystem.Models
{
    public class StockDetails
    {
        [Key]
        public int StockId { get; set; }

        [Column(TypeName ="nvarchar(300)")]
        public string StockName { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string TransactionType { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public double Amount { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string TransactionDate { get; set; }
    }
}
